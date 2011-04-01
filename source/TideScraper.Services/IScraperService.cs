using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TideScraper.Services
{
    public interface IScraperService
    {
        void ImportStationsAsync();
        event EventHandler<EventArgs> ImportStationsCompleted;

        void ImportHarmonicPredictionsAsync(int year);
        event EventHandler<EventArgs> ImportHarmonicPredictionsCompleted;

        void UpdateStationTimeZonesAsync();
        event EventHandler<EventArgs> UpdateStationTimeZonesCompleted;
    }
}
