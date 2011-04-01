using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;

namespace TideScraper.Core
{
    public static class ExpandoExtensions
    {
        public static bool HasProperty(this ExpandoObject expando, string property)
        {
            return ((IDictionary<string, object>)expando).ContainsKey(property);
        }


    }
}
