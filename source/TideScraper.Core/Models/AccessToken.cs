using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OAuth.Core;

namespace TideScraper.Core.Models
{
    public class AccessToken : TokenBase
    {
        public string UserName { get; set; }
        public DateTime Expiration { get; set; }
    }
}
