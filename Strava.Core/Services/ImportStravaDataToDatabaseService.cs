using System;
using System.Threading.Tasks;

namespace Strava.Core.Services
{
    public interface IImportStravaDataToDatabaseService
    {
        Task Import();
    }

    public class ImportStravaDataToDatabaseService : IImportStravaDataToDatabaseService
    {
        private readonly IActivityService _activityService;

        public ImportStravaDataToDatabaseService(IActivityService activityService)
        {
            _activityService = activityService;
        }
        
        public async Task Import()
        {
            
            try
            {

                var activities = await _activityService.GetAll();

            }
            catch (Exception)
            {
                throw;
            }
        }
        
    }
}