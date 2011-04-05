using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TideScraper.Core
{
    public class ValidationError
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
