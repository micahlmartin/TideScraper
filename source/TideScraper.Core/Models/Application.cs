using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TideScraper.Core.Models
{
    public class Application
    {
        public object _id { get; set; } 
        public string ApplicationId { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationUrl { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public string AccountId { get; set; }
    }
}
