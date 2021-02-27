using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using Microsoft.Extensions.Options;
using Strava.Core.Models;

namespace Strava.Core.Services
{
    public interface IGoogleSheetService
    {
        Task Write(string sheetId, string sheetRange, IList<IList<object>> values);
        Task<IList<IList<object>>> GetValues(string sheetId, string sheetRange);
    }

    public class GoogleSheetService : IGoogleSheetService
    {
        private readonly GoogleSheetServiceSettings _settings;

        public GoogleSheetService(IOptions<GoogleSheetServiceSettings> options)
        {
            _settings = options.Value;
        }

        public async Task Write(string sheetId, string sheetRange, IList<IList<object>> values)
        {

            SheetsService sheetsService = await GetSheetService();

            var valueRange = new ValueRange()
            {
                Values = values
            };

            SpreadsheetsResource.ValuesResource.UpdateRequest request = sheetsService.Spreadsheets.Values.Update(valueRange, sheetId, sheetRange);

            request.ValueInputOption =
                SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;

            UpdateValuesResponse response = await request.ExecuteAsync();

        }

        public async Task<IList<IList<object>>> GetValues(string sheetId, string sheetRange)
        {

            SheetsService sheetsService = await GetSheetService();

            SpreadsheetsResource.ValuesResource.GetRequest request = sheetsService.Spreadsheets.Values.Get(sheetId, sheetRange);

            ValueRange response = await request.ExecuteAsync();

            IList<IList<object>> results = response.Values;

            return results;

        }
        
        private async Task<SheetsService> GetSheetService()
        {
            
            ICredential credential = await GetUserCredentials();

            SheetsService sheetsService = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = _settings.ApplicationName
            });

            return sheetsService;

        }

        private async Task<ICredential> GetUserCredentials()
        {
            
            string credentialsPath = _settings.CredentialLocation;

            ClientSecrets clientSecrets = new ClientSecrets()
            {
                ClientId = _settings.ClientId,
                ClientSecret = _settings.ClientSecret
            };

            UserCredential credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                clientSecrets,
                new List<string>() {SheetsService.Scope.Spreadsheets},
                "User",
                CancellationToken.None,
                new FileDataStore(credentialsPath, true)
            );

            return credential;
            
        }
    }
}