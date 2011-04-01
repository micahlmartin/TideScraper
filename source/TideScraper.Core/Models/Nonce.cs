using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TideScraper.Core.Models
{
    public class Nonce
    {
        public string Context { get; set; }
        public string Code { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
