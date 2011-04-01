using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TideScraper.Core.Models;
using MongoDB.Driver;
using TideScraper.Core;

namespace TideScraper.Data
{
    public interface ITideRepository
    {
        Station GetStation(int stationId);
        IEnumerable<Station> GetHarmonicStations();

        IEnumerable<Prediction> GetDailyPredictions(int stationId, DateTime date);
        void DeleteAllPredictions(int year);
        void CreatePredictions(int stationId, int year, IEnumerable<Prediction> predictions);

        GeoNearResult<Station> GetNearbyStations(double latitude, double longitude, double range, string measurement, int maxItems);

        IEnumerable<Station> GetStations(IMongoQuery query);
        void Save(IEnumerable<Station> stations);
    }
}
