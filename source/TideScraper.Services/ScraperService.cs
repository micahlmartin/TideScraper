using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Dynamic;
using TideScraper.Core;
using System.Net;
using HtmlAgilityPack;
using System.Threading;
using TideScraper.Core.Models;
using TideScraper.Data;

namespace TideScraper.Services
{
    public class ScraperService : IScraperService
    {
        private ITideRepository _tideRepository;
        private IGeoService _geoService;

        public ScraperService()
        {
            _tideRepository = new MongoTideRepository();
            _geoService = new GeoService();
        }

        public void ImportStationsAsync()
        {
            ThreadPool.QueueUserWorkItem((e) =>
            {

                var url = Settings.StationIndexUrl;
                var client = new WebClient();
                var result = client.DownloadString(url);

                var html = new HtmlAgilityPack.HtmlDocument();
                html.LoadHtml(result);
                var content = html.DocumentNode.SelectSingleNode("//b").InnerHtml;
                var rows = content.Split(new string[] { "\n" }, System.StringSplitOptions.RemoveEmptyEntries).Skip(2).ToList(); //split and remove headers

                var stations = new List<Station>();

                var regex = new Regex(@"(?<id>\d{7})\s{2}(?<dcp>\d)\s{2}(?<name>.+?)(?<lat>\d+\s+\d+\.\d+\s+\w?)\s+(?<long>\d+\s+\d+\.\d+\s+\w)\s+(?<install>\d{2}/\d{2}/\d{4})?\s+(?<remove>\d{2}/\d{2}/\d{4})?\s+(?<benchmark>\d{2}/\d{2}/\d{4})?\s+(?<epoch>\d{4}-\d{4})?");

                foreach (var row in rows)
                {
                    var match = regex.Match(row);
                    if (!match.Success) continue;

                    var station = new Station();
                    station._id = ObjectId.GenerateNewId();
                    station.StationId = int.Parse(match.Groups["id"].Value);
                    station.Name = match.Groups["name"].Value.Trim();
                    station.Location = new Location(DirectionUtils.Parse(match.Groups["long"].Value.Trim()), DirectionUtils.Parse(match.Groups["lat"].Value.Trim()));
                    //station.GMTOffset = _geoService.GetGMTOffset(station.Location.Latitude, station.Location.Longitude);

                    if (match.Groups["install"].Success)
                        station.InstallDate = DateTime.Parse(match.Groups["install"].Value.Trim());

                    if (match.Groups["remove"].Success)
                        station.RemovalDate = DateTime.Parse(match.Groups["remove"].Value.Trim());

                    stations.Add(station);
                }

                stations = stations.Distinct().ToList();

                stations.AsParallel().ForAll(ScrapeStationInfo);

                RecreateCollection(stations);

                OnImportStationsCompleted();
            });
        }

        private void ScrapeStationInfo(Station station)
        {
            var stationId = station.StationId;

            var client = new WebClient();
            try
            {
                var result = client.DownloadString(Settings.GetStationDetailUrl(stationId));

                var referenceStationMatch = ReferencedStationRegex.Match(result);

                if (referenceStationMatch.Success)
                {
                    station.ReferencedStationId = int.Parse(referenceStationMatch.Groups["stationid"].Value);

                    var offsetMatch = OffsetRegex.Match(result);
                    station.HeightOffset = new Offset(double.Parse(offsetMatch.Groups["heighthigh"].Value), double.Parse(offsetMatch.Groups["heightlow"].Value));
                    station.TimeOffset = new Offset(int.Parse(offsetMatch.Groups["timehigh"].Value),int.Parse(offsetMatch.Groups["timelow"].Value));
                }
                else
                    station.IsHarmonic = true;

                station.IsComplete = true;
            }
            catch (Exception)
            {
                station.IsComplete = false;
            }
        }
        private static Regex ReferencedStationRegex = new Regex(@"Referenced to Station.+\(\s(?<stationid>\d{7})\s\)");
        private static Regex OffsetRegex = new Regex(@"Height offset in feet.+?low:.+?(?<heightlow>-?[0-9]+(?:\.[0-9]*)?).+?high:.+?.+?(?<heighthigh>-?[0-9]+(?:\.[0-9]*)?).+?Time offset in mins.+?low:(?<timelow>-?\d+).+?high:.+?(?<timehigh>-?\d+)");

        private void RecreateCollection(IEnumerable<Station> stations)
        {
            var server = MongoServer.Create(Settings.TidesMongoConnection);
            server.Connect();

            var db = server["tides"];
            if (db.CollectionExists("stations"))
                db.DropCollection("stations");


            var stationCollection = db["stations"];
            stationCollection.InsertBatch(stations);
            stationCollection.EnsureIndex(IndexKeys.GeoSpatial("Location"));

            server.Disconnect();   
        }

        public event EventHandler<EventArgs> ImportStationsCompleted;
        protected void OnImportStationsCompleted()
        {
            var importCompletedEvent = ImportStationsCompleted;
            if (ImportStationsCompleted != null)
                ImportStationsCompleted(this, EventArgs.Empty);
        }

        public void ImportHarmonicPredictionsAsync(int year)
        {
            ThreadPool.QueueUserWorkItem((e) =>
            {
                _tideRepository.DeleteAllPredictions(year);

                var stations = _tideRepository.GetHarmonicStations().Distinct();

                stations.AsParallel().ForAll(station =>
                {
                    var predictionsClient = new PredictionsService.PredictionsPortTypeClient();
                    var predictionData = predictionsClient.getPredictions(new PredictionsService.Parameters
                    {
                        beginDate = new DateTime(year, 1, 1).ToUniversalTime().ToString("yyyyMMdd"),
                        dataInterval = 60,
                        datum = 0,
                        endDate = new DateTime(year, 12, 31).ToUniversalTime().ToString("yyyyMMdd"),
                        stationId = station.StationId.ToString(),
                        timeZone = 0, //GMT
                        unit = 0
                    });

                    var predictions = predictionData.data.Select(x => new Prediction
                    {
                        _id = ObjectId.GenerateNewId(),
                        Height = x.pred,
                        StationId = station.StationId,
                        TimeStamp = DateTime.SpecifyKind(DateTime.Parse(x.timeStamp), DateTimeKind.Utc)
                    }).ToList();

                    var highlowService = new HighLowPredictionsService.highlowtidepredPortTypeClient();
                    var highlowData = highlowService.getHighLowTidePredictions(new HighLowPredictionsService.Parameters
                    {
                        beginDate = new DateTime(year, 1, 1).ToString("yyyyMMdd"),
                        datum = 0,
                        endDate = new DateTime(year, 12, 31).ToString("yyyyMMdd"),
                        stationId = station.StationId.ToString(),
                        timeZone = 1, //GMT
                        unit = 0
                    });

                    foreach (var highlowval in highlowData.HighLowValues1)
                    {
                        foreach (var hlData in highlowval.data)
                        {
                            predictions.Add(new Prediction
                            {
                                _id = ObjectId.GenerateNewId(),
                                Height = hlData.pred,
                                Level = hlData.type == "L" ? TideLevel.Low : TideLevel.High,
                                TimeStamp = DateTime.SpecifyKind(DateTime.Parse(highlowval.date + " " + hlData.time), DateTimeKind.Utc),
                                StationId = station.StationId
                            });
                        }
                    }

                    predictions = predictions.OrderBy(x => x.TimeStamp).ToList();
                    SetTideLevel(predictions);

                    _tideRepository.CreatePredictions(station.StationId, year, predictions);
                });                

                OnImportHarmonicPredictionsCompleted();
            });

        }

        private void SetTideLevel(List<Prediction> predictions)
        {
            var stack = new List<Prediction>();
            for (int i = 0; i < predictions.Count; i++)
            {
                var currentPrediction = predictions[i];
                if (currentPrediction.Level == TideLevel.Unknown)
                    stack.Add(currentPrediction);
                else
                {
                    var level = currentPrediction.Level == TideLevel.High ? TideLevel.Rising : TideLevel.Falling;
                    foreach (var prediction in stack) prediction.Level = level;
                    stack.Clear();
                }
            }
            if (stack.Count > 0)
            {
                var lastLevel = predictions.Last(x => x.Level == TideLevel.High || x.Level == TideLevel.Low).Level;
                var level = lastLevel == TideLevel.High ? TideLevel.Falling : TideLevel.Rising;
                foreach (var prediction in stack) prediction.Level = level;
                stack.Clear();
            }
        }

        public event EventHandler<EventArgs> ImportHarmonicPredictionsCompleted;
        protected void OnImportHarmonicPredictionsCompleted()
        {
            var importCompletedEvent = ImportHarmonicPredictionsCompleted;
            if (importCompletedEvent != null)
                ImportHarmonicPredictionsCompleted(this, EventArgs.Empty);
        }

        public void UpdateStationTimeZonesAsync()
        {
            var stations = _tideRepository.GetStations(Query.EQ("GMTOffset", BsonNull.Value));

            foreach (var station in stations)
            {
                station.GMTOffset = _geoService.GetGMTOffset(station.Location.Latitude, station.Location.Longitude);
            }

            _tideRepository.Save(stations);

            OnUpdateStationTimeZonesCompleted();
        }

        public event EventHandler<EventArgs> UpdateStationTimeZonesCompleted;
        protected void OnUpdateStationTimeZonesCompleted()
        {
            var updateCompletedEvent = UpdateStationTimeZonesCompleted;
            if (updateCompletedEvent != null)
                updateCompletedEvent(this, EventArgs.Empty);
        }
    }
}
