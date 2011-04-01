using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TideScraper.Services
{
    public class ServiceResult<TResults>
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public TResults Result { get; set; }

        public static ServiceResult<TResults> Failed = new ServiceResult<TResults>();
    }
}
