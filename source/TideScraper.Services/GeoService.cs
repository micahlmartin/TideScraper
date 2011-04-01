using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TideScraper.Core;
using System.Net;
using System.Xml.Linq;

namespace TideScraper.Services
{
    public class GeoService : IGeoService
    {
        public double? GetGMTOffset(double latitude, double longitude)
        {
            var url = Settings.GetGeoNamesTimezoneUrl(latitude, longitude, Settings.GeoNamesUsername);
            var client = new WebClient();
            
            var result = client.DownloadString(url);
            var doc = XDocument.Parse(result);

            try
            {
                return double.Parse(doc.Element("geonames").Element("timezone").Element("gmtOffset").Value);
            }
            catch (Exception)
            {
                return new Nullable<double>();
            }
        }
    }
}
