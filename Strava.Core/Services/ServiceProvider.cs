using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Strava.Core.Data;
using Strava.Core.Models;

namespace Strava.Core.Services
{
    public static class ServiceProvider
    {
        
        private static readonly Microsoft.Extensions.DependencyInjection.ServiceProvider Provider;

        static ServiceProvider()
        {
            
            Provider = SetUp();
            
        }


        public static T Get<T>()
        {

            T service = Provider.GetService<T>();

            return service;

        }

        private static Microsoft.Extensions.DependencyInjection.ServiceProvider SetUp()
        {
            
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            Microsoft.Extensions.DependencyInjection.ServiceProvider serviceProvider = new ServiceCollection()
                .Configure<ConnectionStrings>(configuration.GetSection(ConnectionStrings.ConfigSection))
                .Configure<StravaClientSettings>(configuration.GetSection(StravaClientSettings.ConfigSection))
                .Configure<GoogleSheetServiceSettings>(configuration.GetSection(GoogleSheetServiceSettings.ConfigSection))
                .AddDbContext<StravaDbContext>()
                .AddScoped<IStravaApiTransactionService, StravaApiTransactionService>()
                .AddScoped<IStravaClientService, StravaClientService>()
                .AddScoped<IStravaTokenService, StravaTokenService>()
                .AddScoped<IAthleteApiService, AthleteApiService>()
                .AddScoped<IActivityApiService, ActivityApiService>()
                .AddScoped<IActivityService, ActivityService>()
                .AddScoped<IImportStravaDataToDatabaseService, ImportStravaDataToDatabaseService>()
                .AddScoped<IGoogleSheetService, GoogleSheetService>()
                .AddScoped<IExportStravaDataToGoogleSheetService, ExportStravaDataToGoogleSheetService>() 
                .BuildServiceProvider();
            
            return serviceProvider;
            
        }        
        
    }
}