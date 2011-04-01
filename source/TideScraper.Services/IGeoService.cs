using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TideScraper.Services
{
    public interface IGeoService
    {
        double? GetGMTOffset(double latitude, double longitude);
    }
}
