using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TideScraper.Core.Security;

namespace TideScraper.Core.Models
{
    public class Consumer : IConsumer
    {
        public string ConsumerKey { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }
}
