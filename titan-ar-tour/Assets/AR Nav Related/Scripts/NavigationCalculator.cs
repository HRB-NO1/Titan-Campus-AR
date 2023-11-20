using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
namespace CSUF_AR_Navigation
{
    public class NavigationCalculator
    {
        public static double getDistance(double lat1, double lng1, double lat2, double lng2)
        {
            var d1 = lat1 * (Math.PI / 180.0);
            var num1 = lng1 * (Math.PI / 180.0);
            var d2 = lat2 * (Math.PI / 180.0);
            var num2 = lng2 * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
                     Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }

        public static double getHeading(double lat1, double lng1, double lat2, double lng2)
        {
            var dLon = ToRad(lng2 - lng1);
            var dPhi = Math.Log(
                Math.Tan(ToRad(lat2)/2+Math.PI/4)/Math.Tan(ToRad(lat1)/2+Math.PI/4));
            if (Math.Abs(dLon) > Math.PI) 
                dLon = dLon > 0 ? -(2*Math.PI-dLon) : (2*Math.PI+dLon);
            return ToBearing(Math.Atan2(dLon, dPhi));
        }

        public static double ToRad(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        public static double ToDegrees(double radians)
        {
            return radians * 180 / Math.PI;
        }

        public static double ToBearing(double radians) 
        {  
            // convert radians to degrees (as bearing: 0...360)
            return (ToDegrees(radians) +360) % 360;
        }
    }
}