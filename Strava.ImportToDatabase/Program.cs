using System.Threading.Tasks;
using Strava.Core.Services;

namespace Strava.ImportToDatabase
{
    class Program
    {
        static async Task Main(string[] args)
        {
            
            await StartProcess();
            
        }

        private static async Task StartProcess()
        {

            var importService = ServiceProvider.Get<IImportStravaDataToDatabaseService>();

            await importService.Import();
            
        }

    }
}