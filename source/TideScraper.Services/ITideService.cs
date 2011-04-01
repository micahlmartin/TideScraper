using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TideScraper.Core.Models;

namespace TideScraper.Services
{
    public interface ITideService
    {
        ServiceResult<dynamic> GetStationsNearby(double lat, double lon, double range, string measurement, int maxItems);

        ServiceResult<dynamic> GetPredictionByTime(int stationId, DateTime time);

        ServiceResult<dynamic> GetPredictionsNearby(double lat, double lon, double range, string measurement, DateTime time, int maxItems);

        ServiceResult<dynamic> GetHighLowPredictions(int stationId, DateTime time);
    }
}
