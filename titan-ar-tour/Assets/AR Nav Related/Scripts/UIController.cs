using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
namespace CSUF_AR_Navigation
{
    public class UIController : MonoBehaviour
    {
        public static VisualElement root;
        public static VisualElement DistanceBackground;
        public static VisualElement CompletedButtonContainer;
        public static TextField DestinationInput;
        public static Label CurrentStepText;
        public static Label DistanceText;
        private void OnEnable()
        {
            root = GetComponent<UIDocument>().rootVisualElement;
            CurrentStepText = root.Q<Label>("CurrentStepLabel");
            DistanceText = root.Q<Label>("DistanceLabel");
            DistanceBackground = root.Q<VisualElement>("BottomTextContainer");
            CompletedButtonContainer = root.Q<VisualElement>("CompletedTextContainer");
            DestinationInput = root.Q<TextField>("DestinationInput");
            Button DestinationButton = root.Q<Button>("DestinationButton");
            // Button DismissButton = root.Q<Button>("DismissButton");
            DistanceBackground.style.visibility = Visibility.Hidden;
            CompletedButtonContainer.style.visibility = Visibility.Hidden;

            // Get reference to the CampusTourButton
            Button CampusTourButton = root.Q<Button>("CampusTourButton");

            // Add listener for CampusTourButton click event
            if (CampusTourButton != null)
            {
                CampusTourButton.clicked += ChangeScene;
            }

            DestinationButton.clicked += sendDestination;
            // DismissButton.clicked += resetNavigation;

            // added for auto destination from last scene 
            if (PlayerPrefs.HasKey("Destination"))
            {
                string destination = PlayerPrefs.GetString("Destination");
                DestinationInput.value = destination; // set the input field value
                sendDestination(); // automatically initialize navigation
            }
        }

        private static void ChangeScene()
        {
            Debug.Log("Campus Tour Button clicked. Loading 'AR Tuffy' scene.");
            SceneManager.LoadScene("AR Tuffy");
        }

        public static void showTrackingInfo(bool initialized, string status, string destination, string currentStep, string distance)
        {
            if (!initialized) // If the navigation has not been initialized, let the user know that it is pending
            {
                CurrentStepText.text = "Loading navigation to <b>" + destination + "</b>";
                // OutputText.text = "Loading...\nLow Tracking Accuracy\n";
            }
            else // The normal display of information to the user
            {
                DistanceBackground.style.visibility = Visibility.Visible;
                CurrentStepText.text = currentStep + "\n" + status;
                DistanceText.text = distance + " meters away";
            }
        }

        private static void resetNavigation()
        {
            CompletedButtonContainer.style.visibility = Visibility.Hidden;
            DistanceBackground.style.visibility = Visibility.Hidden;
            DestinationInput.style.display = DisplayStyle.Flex;
            CurrentStepText.text = "Enter your target destination";
        }

        public static void displayNavComplete(string msg)
        {
            CurrentStepText.text = msg;
            CompletedButtonContainer.style.visibility = Visibility.Visible;
            DistanceBackground.style.visibility = Visibility.Hidden;
        }

        private static void sendDestination()
        {
            // Here, instead of just hiding the input, 
            // we're also checking if its value is not empty 
            // before proceeding.
            if (!string.IsNullOrEmpty(DestinationInput.text))
            {
                DestinationInput.style.display = DisplayStyle.None;
                SampleScript.setDestination(DestinationInput.text);
                CurrentStepText.text = "Loading...";
            }
            else
            {
                retrySendDestination();
            }
        }

        public static void retrySendDestination()
        {
            DestinationInput.style.display = DisplayStyle.Flex;
            CurrentStepText.text = "There was an error finding your destination, please try again";
        }
    }
}