using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Google.XR.ARCoreExtensions;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
namespace CSUF_AR_Navigation
{
    public class SampleScript : MonoBehaviour
    {
        // Tracking information using GeospatialAPI
        public AREarthManager EarthManager;
        //GeospatialAPI and ARCore initialization and results
        public VpsInitializer Initializer;
        // Allowable accuracy of azimuth
        public double HeadingThreshold = 25;
        // Allowable accuracy of horizontal position
        public double HorizontalThreshold = 20;
        public static String Destination; //String input value of the destination to navigate to
        double Altitude; // Height to place the object
        public double Heading; // Object orientation (North = 0 degrees)
        public GameObject IntermediaryPrefab; // Data of intermediary display object
        public GameObject MarkerPrefab; // Data of marker display object
        GameObject displayCurrentStep; // Actual marker object to be displayed
        public ARAnchorManager AnchorManager; // Used to create anchors
        Queue<NavSteps> Steps; // The list of steps of the navigation
        Queue<IntermediaryPoint> Intermediaries; // The list of intermediary arrows between points
        NavSteps CurrentStep; // The current step in the navigation
        IntermediaryPoint CurrentIntermediary; // The closest arrow to the user's position
        GameObject displayCurrentIntermediary; // Actual intermediary object to be displayed
        bool Initialized; // Whether or not the navigation has begun
        bool NavComplete;
        public static bool RetrievedDestination = false;

        void Start()
        {
            Steps = new Queue<NavSteps>();
            Intermediaries = new Queue<IntermediaryPoint>();
            Initialized = false;
            NavComplete = false;
        }
        // Update is called once per frame
        void Update()
        {
            string status = "";
            // If initialization fails or you do not want to track, do nothing and return
            if (!Initializer.IsReady || EarthManager.EarthTrackingState != TrackingState.Tracking || !RetrievedDestination || NavComplete)
            {
                return;
            }
            // Get tracking results
            GeospatialPose pose = EarthManager.CameraGeospatialPose;
            Altitude = pose.Altitude - 3f;

            // Tracking accuracy is worse than the threshold (larger value)
            if (pose.HeadingAccuracy > HeadingThreshold || pose.HorizontalAccuracy > HorizontalThreshold)
            {
                status = "Low Tracking accuracy";
            }
            else 
            {
                status = "High Tracking Accuracy";
                if (!Initialized) // If the directions have not been initialized, initialize
                {
                    initializeRouting(pose); // Eventually put in a try/catch block
                }

                if (status == "End" || !Initialized)
                {
                    RetrievedDestination = false;
                    return;
                }   

                if (displayCurrentStep != null) // If there are more steps in the navigation, continue to update
                {
                    updateCurrentStep(pose);
                }
                else // Else, the navigation is complete
                {
                    status = "Destination reached!";
                }
            }

            if (status == "Destination reached!")
            {
                string msg = "You have arrived at <b>" + Destination + "</b>";
                UIController.displayNavComplete(msg);
                NavComplete = true;
            }
            else
            {
                // Display tracking information regardless of navigation progress
                string computedDistance = NavigationCalculator.getDistance(pose.Latitude, pose.Longitude, CurrentStep.latitude, CurrentStep.longitude).ToString("F1");
                UIController.showTrackingInfo(Initialized, status, Destination, CurrentStep.htmlStep, computedDistance);
            }
        }

        void initializeRouting(GeospatialPose pose)
        {
            Steps = NavigationManager.getDirections(pose, Destination); // Use the navigation manager to make an API call to retrieve the navigation steps
            if (Steps.Count == 0)
            {
                UIController.retrySendDestination();
            }
            else
            {
                CurrentStep = Steps.Dequeue(); // Pop the current navigation step from the queue
                
                // The quaternion determines where the marker should be pointed.  If there are many steps, point the marker to the next marker.
                Quaternion quaternion;
                if (Steps.Count > 0)
                {
                    double heading = NavigationCalculator.getHeading(CurrentStep.latitude, CurrentStep.longitude, Steps.Peek().latitude, Steps.Peek().longitude);
                    quaternion = Quaternion.AngleAxis(180f - (float)heading, Vector3.up);
                }
                else
                {
                    quaternion = Quaternion.AngleAxis(180f - (float)Heading, Vector3.up);
                }
                
                // Once the correct information is gathered, the anchor can then be instantiated
                ARGeospatialAnchor currentAnchor = AnchorManager.AddAnchor(CurrentStep.latitude, CurrentStep.longitude, Altitude, quaternion);
                // Using the anchor information, the GameObject can now be placed into the space
                if (currentAnchor != null)
                {
                    displayCurrentStep = Instantiate(MarkerPrefab, currentAnchor.transform);
                }

                // After the marker is placed, make an API call to generate the points along the current step and instantiate them as well
                Intermediaries = NavigationManager.getIntermediaries(CurrentStep);
                CurrentIntermediary = Intermediaries.Dequeue();
                
                ARGeospatialAnchor currentIntermediaryAnchor = AnchorManager.AddAnchor(CurrentIntermediary.latitude, CurrentIntermediary.longitude, Altitude, CurrentIntermediary.quaternion);
                if (currentIntermediaryAnchor != null)
                {
                    displayCurrentIntermediary = Instantiate(IntermediaryPrefab, currentIntermediaryAnchor.transform);
                }

                Initialized = true;
            }
        }

        void updateCurrentStep(GeospatialPose pose) 
        {
            // This destroys intermediary arrows as the user walks through them so the space isn't cluttered with unnecessary arrows
            double distanceToIntermediary = NavigationCalculator.getDistance(pose.Latitude, pose.Longitude, CurrentIntermediary.latitude, CurrentIntermediary.longitude);
            if (Intermediaries.Count > 0) {
                if (distanceToIntermediary < 10)
                {
                    Destroy(displayCurrentIntermediary);
                    setCurrentIntermediary();
                }
            }
            
            // This checks if the user has reached the marker, and if the current step should be updated
            double distanceToStep = NavigationCalculator.getDistance(pose.Latitude, pose.Longitude, CurrentStep.latitude, CurrentStep.longitude);
            if (distanceToStep < 10) // If within 10 meters of the marker, navigation can be updated
            {
                Destroy(displayCurrentStep); // Destroy the display of the current step
                // Destroy all arrows of the current step to declutter
                cleanupIntermediaries();

                if (Steps.Count > 0)
                {
                    CurrentStep = Steps.Dequeue(); // Dequeue the next step
                    double heading = NavigationCalculator.getHeading(CurrentStep.latitude, CurrentStep.longitude, Steps.Peek().latitude, Steps.Peek().longitude);
                    Quaternion quaternion = Quaternion.AngleAxis(180f - (float)heading, Vector3.up);

                    Intermediaries = NavigationManager.getIntermediaries(CurrentStep);
                    setCurrentIntermediary();
                
                    // Create anchors at specified position and orientation
                    ARGeospatialAnchor currentAnchor = AnchorManager.AddAnchor(CurrentStep.latitude, CurrentStep.longitude, Altitude, quaternion);
                    // Materialize the object if the anchor is correctly created
                    if (currentAnchor != null)
                    {
                        displayCurrentStep = Instantiate(MarkerPrefab, currentAnchor.transform);
                    }
                }
                else 
                {
                    cleanupIntermediaries();
                }
            }
        }

        void setCurrentIntermediary()
        {
            CurrentIntermediary = Intermediaries.Dequeue();
            ARGeospatialAnchor currentIntermediaryAnchor = AnchorManager.AddAnchor(CurrentIntermediary.latitude, CurrentIntermediary.longitude, Altitude, CurrentIntermediary.quaternion);
            if (currentIntermediaryAnchor != null)
            {
                displayCurrentIntermediary = Instantiate(IntermediaryPrefab, currentIntermediaryAnchor.transform);
            }
        }

        public static void setDestination(string destination)
        {
            Destination = destination;
            RetrievedDestination = true;
        }

        void reset()
        {
            cleanupIntermediaries();
            cleanupSteps();
            Initialized = NavComplete = RetrievedDestination = false;
        }

        void cleanupIntermediaries()
        {
            Destroy(displayCurrentIntermediary);
            Intermediaries = new Queue<IntermediaryPoint>();
        }

        void cleanupSteps()
        {
            Destroy(displayCurrentStep);
            Steps = new Queue<NavSteps>();
        }
    }
}


