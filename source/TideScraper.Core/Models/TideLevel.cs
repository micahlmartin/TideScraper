using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TideScraper.Core.Models
{
    public enum TideLevel
    {
        Low = -2,
        Falling = -1,
        Unknown = 0,
        Rising = 1,
        High = 2
    }
}
