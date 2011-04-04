using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TideScraper.Services;
using TideScraper.Core;
using TideScraper.Web.Api.Security;

namespace TideScraper.Web.Api.Controllers
{
    public class ApiController : AsyncController
    {
        private ITideService _tideService;

        public ApiController(ITideService tideService)
        {
            _tideService = tideService;
        }

        public ActionResult NearbyStations(double lat, double lng, double range, string measurement, int? limit)
        {
            var results = _tideService.GetStationsNearby(lat, lng, range, measurement ?? Settings.DefaultMeasurement, limit ?? Settings.DefaultStationLimit);

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [RequireAuth]
        public ActionResult StationsKml()
        {
            return View();
        }

        public ActionResult GetPrediction(int stationId, DateTime time)
        {           
            var prediction = _tideService.GetPredictionByTime(stationId, time);
            return Json(prediction, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPredictionsNearby(double lat, double lng, double range, string measurement, DateTime time, int? limit)
        {
            var results = _tideService.GetPredictionsNearby(lat, lng, range, measurement ?? Settings.DefaultMeasurement, time, limit ?? Settings.DefaultStationLimit);
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetHighLowPredictions(int stationId, DateTime time)
        {
            var predictions = _tideService.GetHighLowPredictions(stationId, time);
            return Json(predictions, JsonRequestBehavior.AllowGet);
        }
    }
}
