using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TideScraper.Core.Security
{
    public static class AuthorizationTokens
    {
        public const string TimeStamp = "auth_timestamp";
        public const string PublicKey = "auth_key";
        public const string Signature = "auth_signature";
        public const string SignatureMethod = "auth_signature_method";
    }
}