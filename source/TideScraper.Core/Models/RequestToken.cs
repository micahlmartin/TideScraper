using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OAuth.Core;

namespace TideScraper.Core.Models
{
    public class RequestToken : TokenBase
    {
        public bool IsAccessDenied { get; set; }
        public bool IsExpired { get; set; }
        public AccessToken AccessToken { get; set; }
    }
}
