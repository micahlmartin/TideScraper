using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TideScraper.Core.Models
{
    public class Location
    {
        public Location() : this(0, 0) { }
        public Location(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        public double Longitude { get; set; }
        public double Latitude { get; set; }
        

        public override bool Equals(object obj)
        {
            var objToCompare = obj as Location;
            if (objToCompare == null) return false;

            return Latitude.Equals(objToCompare.Latitude) && Longitude.Equals(objToCompare.Longitude);
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + Latitude.GetHashCode();
            hash = (hash * 7) + Longitude.GetHashCode();
            return hash;
        }

        public static bool operator ==(Location a, Location b)
        {
            if (object.ReferenceEquals(a, b)) return true;

            if (((object)a == null) || ((object)b == null)) return false;

            return a.Latitude == b.Latitude && a.Longitude == b.Longitude;
        }

        public static bool operator !=(Location a, Location b)
        {
            return !(a == b);
        }
    }
}
