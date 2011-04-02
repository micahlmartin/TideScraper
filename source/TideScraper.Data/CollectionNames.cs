using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TideScraper.Data
{
    static class TidesCollectionNames
    {
        public static string Stations = "stations";
        public static string GetPredictionCollectionName(int year)
        {
            return "predictions" + year.ToString();
        }
    }

    static class AuthenticationCollectionNames
    {
        public static string Nonce = "nonce";
        public static string Consumers = "consumers";
        public static string RequestTokens = "request_tokens";
        public static string AccessTokens = "access_tokens";
    }
}
