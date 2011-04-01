using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TideScraper.Services;

namespace TideScraper.Web.Api.Controllers
{
    public class ScraperController : AsyncController
    {
        private IScraperService _scraperService = new ScraperService();

        [NoAsyncTimeout]
        public void ImportStationsAsync()
        {
            AsyncManager.OutstandingOperations.Increment();

            _scraperService.ImportStationsCompleted += (sender, e) =>
            {
                AsyncManager.OutstandingOperations.Decrement();
            };

            _scraperService.ImportStationsAsync();
        }
        public ActionResult ImportStationsCompleted()
        {
            return Json(new { status = "ok" }, JsonRequestBehavior.AllowGet);
        }

        [NoAsyncTimeout]
        public void ImportHarmonicPredictionsAsync(int year)
        {
            AsyncManager.OutstandingOperations.Increment();
            _scraperService.ImportHarmonicPredictionsCompleted += (sender, e) =>
            {
                AsyncManager.OutstandingOperations.Decrement();
            };
            _scraperService.ImportHarmonicPredictionsAsync(year);
        }
        public ActionResult ImportHarmonicPredictionsCompleted()
        {
            return Json(new { status = "ok" }, JsonRequestBehavior.AllowGet);
        }

        [NoAsyncTimeout]
        public void UpdateStationTimeZonesAsync()
        {
            AsyncManager.OutstandingOperations.Increment();
            _scraperService.UpdateStationTimeZonesCompleted += (sender, e) =>
            {
                AsyncManager.OutstandingOperations.Decrement();
            };
            _scraperService.UpdateStationTimeZonesAsync();
        }
        public ActionResult UpdateStationTimeZonesCompleted()
        {
            return Json(new { status = "ok" });
        }

    }
}
