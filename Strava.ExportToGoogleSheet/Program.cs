using System;
using System.Threading.Tasks;
using Strava.Core.Services;

namespace Strava.ExportToGoogleSheet
{
    class Program
    {
        static async Task Main(string[] args)
        {
            
            await StartProcess();
            
        }

        private static async Task StartProcess()
        {

            var exportService = ServiceProvider.Get<IExportStravaDataToGoogleSheetService>();

            await exportService.Export();
            
        }
    }
}