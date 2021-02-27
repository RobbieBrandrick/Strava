using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Strava.Core.Extensions;
using Strava.Core.Models;

namespace Strava.Core.Services
{
    public interface IExportStravaDataToGoogleSheetService
    {
        Task Export();
    }

    public class ExportStravaDataToGoogleSheetService : IExportStravaDataToGoogleSheetService
    {
        private readonly IActivityService _activityService;
        private readonly IGoogleSheetService _sheetService;

        public ExportStravaDataToGoogleSheetService(IActivityService activityService, IGoogleSheetService sheetService)
        {
            _activityService = activityService;
            _sheetService = sheetService;
        }
        
        public async Task Export()
        {

            List<Activity> activities = _activityService
                .GetAllFromCache()
                .OrderByDescending(e => e.LocalDate)
                .ToList();

            IList<IList<object>> values = activities.ToGoogleSheetObjects();
            
            await _sheetService.Write("1tWLExrJZriaJXNNLmMbmO6YdcSw1kqFxsvHRebQJDcI", "'Raw Data'!A:Z", values);

        }

    }
}