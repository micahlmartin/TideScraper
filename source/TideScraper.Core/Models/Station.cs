using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TideScraper.Core.Models
{
    public class Station
    {
        public object _id { get; set; }
        public int StationId { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public DateTime? InstallDate { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? RemovalDate { get; set; }
        public DateTime? BenchmarkDate { get; set; }
        public int? ReferencedStationId { get; set; }
        public Offset HeightOffset { get; set; }
        public Offset TimeOffset { get; set; }
        public bool IsHarmonic { get; set; }
        public bool IsComplete { get; set; }
        public double? GMTOffset { get; set; }

        public override bool Equals(object obj)
        {
            var objToCompare = obj as Station;
            if(objToCompare == null) return false;

            return StationId == objToCompare.StationId && Location == objToCompare.Location && HeightOffset == objToCompare.HeightOffset && TimeOffset == objToCompare.TimeOffset;
        }

        public override int GetHashCode()
        {
            int hash = 13;

            hash = (hash * 7) + StationId.GetHashCode();

            if (Location != null)
                hash = (hash * 7) + Location.GetHashCode();

            if (HeightOffset != null)
                hash = (hash * 7) + HeightOffset.GetHashCode();

            if (TimeOffset != null)
                hash = (hash * 7) + TimeOffset.GetHashCode();

            return hash;
        }

        public static bool operator ==(Station a, Station b)
        {
            if (object.ReferenceEquals(a, b)) return true;

            if (((object)a == null) || ((object)b == null)) return false;

            return a.StationId == b.StationId && a.Location == b.Location && a.HeightOffset == b.HeightOffset && a.TimeOffset == b.TimeOffset;
        }

        public static bool operator !=(Station a, Station b)
        {
            return !(a == b);
        }


        
    }
}
