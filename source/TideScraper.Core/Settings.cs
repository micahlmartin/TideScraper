using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace TideScraper.Core
{
    public static class Settings
    {
        public static string TidesMongoConnection
        {
            get
            {
                return ConfigurationManager.AppSettings["TidesMongoConnection"] as string;
            }
        }

        public static string StationIndexUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["StationIndexUrl"] as string;
            }
        }

        public static string GetStationDetailUrl(int stationID)
        {
            return string.Format(ConfigurationManager.AppSettings["StationDetailUrl"], stationID);
        }

        public static string GetGeoNamesTimezoneUrl(double latitude, double longitude, string username)
        {
            return string.Format(ConfigurationManager.AppSettings["GeoNamesTimezoneUrl"], latitude, longitude, username);
        }

        public static string GeoNamesUsername
        {
            get { return ConfigurationManager.AppSettings["GeoNamesUsername"]; }
        }

        public static string DefaultMeasurement
        {
            get { return ConfigurationManager.AppSettings["DefaultMeasurement"]; }
        }

        public static int DefaultStationLimit
        {
            get { return int.Parse(ConfigurationManager.AppSettings["DefaultStationLimit"]); }
        }
    }
}
