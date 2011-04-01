using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using TideScraper.Core;
using System.Dynamic;
using TideScraper.Core.Models;
using TideScraper.Data;

namespace TideScraper.Services
{
    public class TideService : ITideService
    {
        private ITideRepository _tideRepository;
        private const string UTCFormat = "yyyy-MM-ddThh:mmZ";


        public TideService()
        {
            _tideRepository = new MongoTideRepository();
        }
        
        public ServiceResult<dynamic> GetStationsNearby(double lat, double lon, double range, string measurement, int maxItems)
        {
            var result = _tideRepository.GetNearbyStations(lat, lon, range, measurement, maxItems);

            if (!result.Ok)
                return ServiceResult<dynamic>.Failed;

            return new ServiceResult<dynamic>
            {
                Success = true,
                Result = result.Hits.Select(x => new
                {
                    Distance = x.Distance * (measurement == Measurement.Kilometers ? EarthRadians.Kilometers : EarthRadians.Miles),
                    Measurement = measurement,
                    Station = new
                    {
                        StationId = x.Document.StationId,
                        Location = x.Document.Location,
                        GMTOffset = x.Document.GMTOffset,
                        HeightOffset = x.Document.HeightOffset,
                        TimeOffset = x.Document.TimeOffset,
                        HarmonicStationId = x.Document.ReferencedStationId
                    }
                })
            };
        }

        public ServiceResult<dynamic> GetPredictionByTime(int stationId, DateTime time)
        {
            var originalStation = _tideRepository.GetStation(stationId);
            if (originalStation == null)
                return new ServiceResult<dynamic> { ErrorMessage = "Station '" + stationId.ToString() + "' does not exist." };

            var harmonicStationId = originalStation.StationId;

            time = TimeZoneInfo.ConvertTime(time, TimeZoneInfo.GetSystemTimeZones().First(x => x.BaseUtcOffset == TimeSpan.FromHours(originalStation.GMTOffset.Value))).ToUniversalTime();

            if (!originalStation.IsHarmonic)
                harmonicStationId = originalStation.ReferencedStationId.Value;

            var predictions = _tideRepository.GetDailyPredictions(harmonicStationId, time.Date);

            var prediction = predictions.Select(pred =>
            {
                GetOffsetPrediction(originalStation, pred);

                return new KeyValuePair<long, Prediction>(Math.Abs((pred.TimeStamp - time).Ticks), pred);
            }).OrderBy(x => x.Key).First().Value;

            return new ServiceResult<dynamic>
            {
                Success = true,
                Result = new
                {
                    StationId = originalStation.StationId,
                    GMTOffset = originalStation.GMTOffset,
                    Prediction = new
                    {
                        Height = prediction.Height,
                        TimeStamp = new DateTimeOffset(TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(prediction.TimeStamp, DateTimeKind.Utc), TimeZoneInfo.GetSystemTimeZones().First(y => y.BaseUtcOffset == TimeSpan.FromHours((int)originalStation.GMTOffset)))).ToString(),
                        Level = prediction.Level.ToString()
                    }
                }
            };
        }

        private static void GetOffsetPrediction(Station station, Prediction pred)
        {
            double timeOffset = 0;
            double heightOffset = 0;

            //Need to apply an offset for subordinate stations
            if (!station.IsHarmonic)
            {
                if (pred.Level == TideLevel.High || pred.Level == TideLevel.Rising)
                {
                    timeOffset = station.TimeOffset.High;
                    heightOffset = station.HeightOffset.High;
                }
                else
                {
                    timeOffset = station.TimeOffset.Low;
                    heightOffset = station.HeightOffset.Low;
                }
            }

            pred.Height += heightOffset;
            pred.TimeStamp = pred.TimeStamp.AddMinutes(timeOffset);
        }

        public ServiceResult<dynamic> GetPredictionsNearby(double lat, double lon, double range, string measurement, DateTime time, int maxItems)
        {
            var nearbyStationsResult = GetStationsNearby(lat, lon, range, measurement, maxItems);
            
            if(!nearbyStationsResult.Success)
                return ServiceResult<dynamic>.Failed;

            var results = new List<dynamic>();

            foreach (dynamic station in nearbyStationsResult.Result)
            {
                var predictionResult = GetPredictionByTime(station.Station.StationId, time);
                if (predictionResult.Success)
                {
                    results.Add(predictionResult.Result);
                }
            }
            
            return new ServiceResult<dynamic>
            {
                Success = true,
                Result = results
            };
        }

        public ServiceResult<dynamic> GetHighLowPredictions(int stationId, DateTime time)
        {
            var station = _tideRepository.GetStation(stationId);
            if (station == null)
                return new ServiceResult<dynamic> { ErrorMessage = "Station '" + stationId.ToString() + "' does not exist." };

            var harmonicStationId = station.StationId;

            if (!station.IsHarmonic)
                harmonicStationId = station.ReferencedStationId.Value;

            var predictions = _tideRepository.GetDailyPredictions(harmonicStationId, time).Where(x => x.Level == TideLevel.High || x.Level == TideLevel.Low).ToList();

            foreach (var prediction in predictions)
                GetOffsetPrediction(station, prediction);

            return new ServiceResult<dynamic>
            {
                Result = new
                {
                    StationId = station.StationId,
                    Predictions = predictions.Select(x => new
                    {
                        Height = x.Height,
                        Level = x.Level.ToString(),
                        TimeStamp = new DateTimeOffset(TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(x.TimeStamp, DateTimeKind.Utc), TimeZoneInfo.GetSystemTimeZones().First(y => y.BaseUtcOffset == TimeSpan.FromHours((int)station.GMTOffset)))).ToString(),
                    }),
                },
                Success = true
            };
        }
    }
}