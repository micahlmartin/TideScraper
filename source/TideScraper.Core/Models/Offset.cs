using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TideScraper.Core.Models
{
    public class Offset
    {
        public Offset() :this(0,0) { }
        public Offset(double high, double low)
        {
            High = high;
            Low = low;
        }

        public double High { get; set; }
        public double Low { get; set; }

        public override bool Equals(object obj)
        {
            var objToCompare = obj as Offset;
            if (objToCompare == null) return false;

            return High == objToCompare.High && Low == objToCompare.Low;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + High.GetHashCode();
            hash = (hash * 7) + Low.GetHashCode();
            return hash;
        }

        public static bool operator ==(Offset a, Offset b)
        {
            if (object.ReferenceEquals(a, b)) return true;

            if (((object)a == null) || ((object)b == null)) return false;

            return a.High == b.High && a.Low == b.Low;
        }

        public static bool operator !=(Offset a, Offset b)
        {
            return !(a == b);
        }
    }
}
