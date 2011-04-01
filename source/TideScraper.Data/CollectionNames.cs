using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TideScraper.Data
{
    static class CollectionNames
    {
        public static string Stations = "stations";
        public static string GetPredictionCollectionName(int year)
        {
            return "predictions" + year.ToString();
        }
    }
}
