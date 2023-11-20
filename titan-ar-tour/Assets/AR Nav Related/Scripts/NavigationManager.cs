using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using Google.XR.ARCoreExtensions;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
namespace CSUF_AR_Navigation
{
    public class NavigationManager
    {
        static String apiKey = "AIzaSyCJv1juPlayfNG086LAUQ4rtwEDqsT7DZA";
        static String baseNavigationUrl = "https://maps.googleapis.com/maps/api/directions/json";
        public static Queue<NavSteps> getDirections(GeospatialPose pose, String destination)
        {
            Queue<NavSteps> steps = new Queue<NavSteps>();
             
            // Generate the URL to make the API call
            String origin = pose.Latitude.ToString() + "," + pose.Longitude.ToString();
            String formattedDest = getPlaceId(pose, destination);
            if (formattedDest != "NOT FOUND")
            {
                String apiUrl = baseNavigationUrl + "?origin=" + origin + "&destination=place_id:" + formattedDest + "&mode=walking&key=" + apiKey;
                Console.WriteLine("COMPLETE URL: " + apiUrl);

                // Parse the JSON response from the URL to generate a queue of navigation steps
                using (var client = new WebClient())
                {
                    string response = client.DownloadString(apiUrl);
                    if (!string.IsNullOrEmpty(response))
                    {
                        JObject json = JObject.Parse(response);
                        Console.WriteLine("STATUS: " + json.SelectToken("status").ToString());
                        if (json.SelectToken("status").ToString() != "OK")
                        {
                            Console.WriteLine("STATUS IS NOT FOUND");
                            return steps;
                        }
                        else
                        {
                            foreach (JToken jtoken in json.SelectToken("routes[0].legs[0].steps"))
                            {   
                                double lat = 0.0;
                                double lng = 0.0;
                                String htmlStep = jtoken.SelectToken("html_instructions").ToString().Split("<div")[0];
                                String polyLine = jtoken.SelectToken("polyline.points").ToString();

                                double.TryParse(jtoken.SelectToken("end_location.lat").ToString(), out lat);
                                double.TryParse(jtoken.SelectToken("end_location.lng").ToString(), out lng);

                                steps.Enqueue(new NavSteps(lat, lng, htmlStep, polyLine));
                            }
                        }
                    }
                }
            }
            return steps;
        }

        public static Queue<IntermediaryPoint> getIntermediaries(NavSteps currentStep)
        {
            Queue<IntermediaryPoint> intermediaries = new Queue<IntermediaryPoint>();
            Queue<Coordinates> interCoords = NavigationManager.DecodePolyline(currentStep.polyLine);

            // Loop through the queue of intermediary points and generate GameObjects from them
            // Do not get the last set of coords, they overlap with the marker
            while (interCoords.Count > 1)
            {
                Coordinates intCoord = interCoords.Dequeue();

                double heading = 0.0;

                if (interCoords.Count > 2)
                {
                    heading = NavigationCalculator.getHeading(intCoord.latitude, intCoord.longitude, interCoords.Peek().latitude, interCoords.Peek().longitude);
                }
                else 
                {
                    heading = NavigationCalculator.getHeading(intCoord.latitude, intCoord.longitude, currentStep.latitude, currentStep.longitude);
                }
                Quaternion quaternion = Quaternion.AngleAxis(180f - (float)heading, Vector3.up);
                intermediaries.Enqueue(new IntermediaryPoint(quaternion, intCoord.latitude, intCoord.longitude));
            }
            return intermediaries;
        }

        public static Queue<Coordinates> DecodePolyline(string polyLine)
        {
            Queue<Coordinates> intermediaryCoords = new Queue<Coordinates>();
            char[] polylineChars = polyLine.ToCharArray();
            int index = 0;

            int currentLat = 0;
            int currentLng = 0;
            int next5bits;
            int sum;
            int shifter;

            while (index < polylineChars.Length)
            {
                // calculate next latitude
                sum = 0;
                shifter = 0;
                do
                {
                    next5bits = (int)polylineChars[index++] - 63;
                    sum |= (next5bits & 31) << shifter;
                    shifter += 5;
                } while (next5bits >= 32 && index < polylineChars.Length);

                if (index >= polylineChars.Length)
                    break;

                currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

                //calculate next longitude
                sum = 0;
                shifter = 0;
                do
                {
                    next5bits = (int)polylineChars[index++] - 63;
                    sum |= (next5bits & 31) << shifter;
                    shifter += 5;
                } while (next5bits >= 32 && index < polylineChars.Length);

                if (index >= polylineChars.Length && next5bits >= 32)
                    break;

                currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

                double computedLat = Convert.ToDouble(currentLat) / 1E5;
                double computedLng = Convert.ToDouble(currentLng) / 1E5;

                Console.WriteLine("POLY COORD: " + computedLat.ToString() + ", " + computedLng.ToString());

                intermediaryCoords.Enqueue(new Coordinates(computedLat, computedLng));
            }

            return intermediaryCoords;
        }

        public static String getPlaceId(GeospatialPose pose, String destination)
        {
            String placeDest = destination.Replace(" ", "%20");
            String origin = "&point:" + pose.Latitude.ToString() + "," + pose.Longitude.ToString();
            String placeUrl = "https://maps.googleapis.com/maps/api/place/findplacefromtext/json?fields=place_id" + origin + "&input=" + placeDest + "&inputtype=textquery&key=" + apiKey;
            Console.WriteLine("PLACE URL: " + placeUrl);

            String placeId = "";
            using (var client = new WebClient())
            {
                string response = client.DownloadString(placeUrl);
                if (!string.IsNullOrEmpty(response))
                {
                    JObject json = JObject.Parse(response);
                    if (json.SelectToken("status").ToString() != "OK")
                    {
                        placeId = "NOT FOUND";
                    }
                    else
                    {
                        placeId = json.SelectToken("candidates[0].place_id").ToString();
                        Console.WriteLine("PLACE ID: " + placeId);
                    }
                }
            }

            return placeId;
        }
    }
}