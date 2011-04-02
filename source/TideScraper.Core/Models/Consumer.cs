using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OAuth.Core.Interfaces;

namespace TideScraper.Core.Models
{
    public class Consumer : IConsumer
    {
        public object _id { get; set; }
        public string ConsumerSecret { get; set; }

        #region << IConsumer Implementation >>
        
        public string ConsumerKey { get; set; }
        public string Realm { get; set; }

        #endregion
    }
}
