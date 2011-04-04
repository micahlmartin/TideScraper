using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TideScraper.Core.Security
{
    public interface IConsumer
    {
        string ConsumerKey { get; set; }
        string PublicKey { get; set; }
        string PrivateKey { get; set; }
    }
}
