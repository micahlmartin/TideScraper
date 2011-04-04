using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;

namespace TideScraper.Core.Security
{
    public class AuthorizeRequestData
    {
        public AuthorizeRequestData(NameValueCollection formValues)
        {
            Timestamp = formValues[AuthorizationTokens.TimeStamp];
            PublicKey = formValues[AuthorizationTokens.PublicKey];
            Signature = formValues[AuthorizationTokens.Signature];
            SignatureMethod = formValues[AuthorizationTokens.SignatureMethod];
        }

        public string Timestamp { get; set; }
        public string PublicKey { get; set; }
        public string Signature { get; set; }
        public string SignatureMethod { get; set; }

        public string GetDataForSignature()
        {
            return string.Join("&", PublicKey, Timestamp, SignatureMethod);
        }
    }
}