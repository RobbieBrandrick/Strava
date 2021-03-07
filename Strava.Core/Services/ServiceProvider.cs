using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Strava.Core.Data;
using Strava.Core.Models;

namespace Strava.Core.Services
{
    public static class ServiceProvider
    {
        private static Microsoft.Extensions.DependencyInjection.ServiceProvider _provider;

        static ServiceProvider()
        {
            
        }

        public static T Get<T>()
        {

            if (_provider == null)
            {
                
                _provider = SetUpServiceProvider();
                
            }
            
            T service = _provider.GetService<T>();

            return service;
            
        }

        public static IConfiguration GetConfiguration()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            return configuration;

        }

        public static IServiceCollection GetServiceCollection(IConfiguration configuration)
        {
            
            IServiceCollection collection = new ServiceCollection()
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
                .AddScoped<IExportStravaDataToGoogleSheetService, ExportStravaDataToGoogleSheetService>();

            return collection;
            
        }
        

        private static Microsoft.Extensions.DependencyInjection.ServiceProvider SetUpServiceProvider()
        {

            IConfiguration configuration = GetConfiguration();
            IServiceCollection serviceCollection = GetServiceCollection(configuration);
            
            Microsoft.Extensions.DependencyInjection.ServiceProvider serviceProvider =
                serviceCollection.BuildServiceProvider();

            return serviceProvider;
            
        }
    }
}