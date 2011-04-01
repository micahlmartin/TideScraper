using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TideScraper.Core.Models
{
    public class Prediction
    {
        public object _id { get; set; }
        public int StationId { get; set; }
        public double Height { get; set; }
        public DateTime TimeStamp { get; set; }
        public TideLevel Level { get; set; }
    }
}
