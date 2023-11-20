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
    public struct Coordinates
    {
        public double latitude; 
        public double longitude; 
        public Coordinates(double latitude, double longitude) 
        { 
            this.latitude = latitude; 
            this.longitude = longitude; 
        } 
    }

    public struct NavSteps
    {
        public double latitude;
        public double longitude;
        public String htmlStep;
        public String polyLine;
        public NavSteps(double latitude, double longitude, String htmlStep, String polyLine)
        {
            this.latitude = latitude; 
            this.longitude = longitude; 
            this.htmlStep = htmlStep;
            this.polyLine = polyLine;
        }
    }

    public struct IntermediaryPoint
    {
        public Quaternion quaternion;
        public double latitude;
        public double longitude;
        public IntermediaryPoint(Quaternion quaternion, double latitude, double longitude)
        {
            this.quaternion = quaternion;
            this.latitude = latitude; 
            this.longitude = longitude; 
        }
    }
}
