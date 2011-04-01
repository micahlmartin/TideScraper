using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using TideScraper.Core;
using MongoDB.Driver.Builders;
using TideScraper.Core.Models;

namespace TideScraper.Data
{
    public class MongoTideRepository : ITideRepository
    {
        public Station GetStation(int stationId)
        {
            var server = MongoServer.Create(Settings.TidesMongoConnection);
            server.Connect();

            var collection = server[DatabaseNames.Tides][TidesCollectionNames.Stations];
            var station = collection.FindOneAs<Station>(Query.EQ("StationId", stationId));
            server.Disconnect();

            return station;
        }

        public IEnumerable<Prediction> GetDailyPredictions(int stationId, DateTime date)
        {
            var server = MongoServer.Create(Settings.TidesMongoConnection);
            server.Connect();

            var collection = server[DatabaseNames.Tides][TidesCollectionNames.GetPredictionCollectionName(date.Year)];
            var query = new QueryDocument();
            var predictions = collection.FindAs<Prediction>(Query.And(Query.EQ("StationId", stationId), Query.GTE("TimeStamp", date.Date/*.ToUniversalTime()*/).LT(date.Date.AddDays(1)/*.ToUniversalTime()*/))).ToList();

            server.Disconnect();

            return predictions;
        }

        public void DeleteAllPredictions(int year)
        {
            var server = MongoServer.Create(Settings.TidesMongoConnection);
            server.Connect();

            var db = server[DatabaseNames.Tides];
            if (db.CollectionExists(TidesCollectionNames.GetPredictionCollectionName(year)))
                db.DropCollection(TidesCollectionNames.GetPredictionCollectionName(year));

            server.Disconnect();
        }

        public IEnumerable<Station> GetHarmonicStations()
        {
            var server = MongoServer.Create(Settings.TidesMongoConnection);
            server.Connect();

            var collection = server[DatabaseNames.Tides][TidesCollectionNames.Stations];

            var stations = collection.FindAs<Station>(Query.EQ("IsHarmonic", true));

            server.Disconnect();

            return stations;
        }

        public void CreatePredictions(int stationId, int year, IEnumerable<Prediction> predictions)
        {
            var server = MongoServer.Create(Settings.TidesMongoConnection);
            server.Connect();

            var db = server[DatabaseNames.Tides];

            var collection = db[TidesCollectionNames.GetPredictionCollectionName(year)];
            collection.EnsureIndex(IndexKeys.Ascending("StationId"));

            collection.InsertBatch<Prediction>(predictions);

            server.Disconnect();
        }

        public GeoNearResult<Station> GetNearbyStations(double latitude, double longitude, double range, string measurement, int maxItems)
        {
            var radians = measurement == Measurement.Kilometers ? EarthRadians.Kilometers: EarthRadians.Miles;

            var server = MongoServer.Create(Settings.TidesMongoConnection);
            server.Connect();

            var stationsCollection = server[DatabaseNames.Tides][TidesCollectionNames.Stations];
            var result = stationsCollection.GeoNearAs<Station>(Query.EQ("IsComplete", true), longitude, latitude, maxItems, GeoNearOptions.SetSpherical(true).SetMaxDistance(range).SetDistanceMultiplier(range / radians));

            server.Disconnect();

            return result;
        }

        public IEnumerable<Station> GetStations(IMongoQuery query)
        {
            var server = MongoServer.Create(Settings.TidesMongoConnection);
            server.Connect();

            var stationsCollection = server[DatabaseNames.Tides][TidesCollectionNames.Stations];
            var result = stationsCollection.FindAs<Station>(query).ToList();

            server.Disconnect();

            return result;
        }


        public void Save(IEnumerable<Station> stations)
        {
            var server = MongoServer.Create(Settings.TidesMongoConnection);
            server.Connect();

            var stationsCollection = server[DatabaseNames.Tides][TidesCollectionNames.Stations];

            foreach (var station in stations)
            {
                stationsCollection.Save<Station>(station);
            }

            server.Disconnect();
        }
    }
}
