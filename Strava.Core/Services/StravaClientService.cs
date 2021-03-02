using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using Strava.Core.Dto;
using Strava.Core.Models;

namespace Strava.Core.Services
{
    public interface IStravaClientService
    {
        Task<T> Execute<T>(RestRequest request, bool includeToken = true);
    }

    public class StravaClientService : IStravaClientService
    {
        
        private const string BaseUrl = "https://www.strava.com";

        private readonly IStravaTokenService _tokenService;
        private readonly IStravaApiTransactionService _transactionService;
        private  readonly StravaClientSettings _settings;
        private StravaToken _token;

        public StravaClientService(
            IStravaTokenService stravaTokenService, 
            IStravaApiTransactionService transactionService,
            IOptions<StravaClientSettings> options)
        {
            _tokenService = stravaTokenService;
            _transactionService = transactionService;
            _settings = options.Value;
        }

        public async Task<T> Execute<T>(RestRequest request, bool includeToken = true)
        {

            ThrowWhenMaxRequestsExceeded();
            
            var client = new RestClient(BaseUrl);

            if (includeToken)
            {
                
                await ValidateToken();

                client.Authenticator = new JwtAuthenticator(_token.AccessToken);

            }
            
            IRestResponse<T> response = await client.ExecuteAsync<T>(request);

            await LogTransaction(client, request, response);
            
            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                
                Exception exception = new Exception(message, response.ErrorException);
                
                throw exception;
                
            }
            
            return response.Data;
            
        }

        private async Task LogTransaction<T>(RestClient client, RestRequest request, IRestResponse<T> response)
        {
            var requestToLog = new
            {
                resource = request.Resource,
                // Parameters are custom anonymous objects in order to have the parameter type as a nice string
                // otherwise it will just show the enum value
                parameters = request.Parameters.Select(parameter => new
                {
                    name = parameter.Name,
                    value = parameter.Value,
                    type = parameter.Type.ToString()
                }),
                // ToString() here to have the method as a nice string otherwise it will just show the enum value
                method = request.Method.ToString(),
                // This will generate the actual Uri used in the request
                uri = client.BuildUri(request),
            };

            var responseToLog = new
            {
                statusCode = response.StatusCode,
                content = response.Content,
                headers = response.Headers,
                // The Uri that actually responded (could be different from the requestUri if a redirection occurred)
                responseUri = response.ResponseUri,
                errorMessage = response.ErrorMessage,
            };

            await _transactionService.Add(request.Resource,
                JsonConvert.SerializeObject(requestToLog), 
                JsonConvert.SerializeObject(responseToLog));
            
        }

        private async Task ValidateToken()
        {

            if (_token == null)
            {
                _token = await _tokenService.GetToken();
            }
            
            if (_token == null || string.IsNullOrWhiteSpace(_token.AccessToken) || DateTime.Now > _token.Expiry)
            {
                await RefreshToken();
            }

        }

        private async Task RefreshToken()
        {

            var request = new RestRequest("/oauth/token");
            
            request.Method = Method.POST;
            request.AddHeader("Accept", "application/json");
            request.AddParameter("client_id", _settings.ClientId);
            request.AddParameter("client_secret", _settings.ClientSecret);

            if (_token != null && !string.IsNullOrWhiteSpace(_token.RefreshToken) && DateTime.Now > _token.Expiry)
            {

                request.AddParameter("refresh_token", _token.RefreshToken);
                request.AddParameter("grant_type", "refresh_token");
                
            }            
            else if (_token == null || string.IsNullOrWhiteSpace(_token.AccessToken))
            {
            
                request.AddParameter("code", _settings.Code);
                request.AddParameter("grant_type", "authorization_code");
                
            }

            TokenDto tokenDto = await Execute<TokenDto>(request, false);

            _token = new StravaToken
            {
                AccessToken = tokenDto?.access_token ?? throw new Exception("Cannot obtain access token from the strava api"),
                RefreshToken = tokenDto?.refresh_token ?? throw new Exception("Cannot obtain refresh token from the strava api"),
                Expiry = DateTime.Now.AddSeconds(tokenDto.expires_in)
            };

            await _tokenService.Add(_token);

        }

        private void ThrowWhenMaxRequestsExceeded()
        {
            
            var oneDayAgo = DateTime.Today.AddDays(-1);

            IQueryable<StravaApiTransaction> transactions = _transactionService.GetAll().Where(e => e.CreateDate >= oneDayAgo);

            if (transactions.Count() >= 1000)
            {
                
                throw new InvalidOperationException(
                    "Cannot query Strava API: Over 1000 transactions were made in the last day.");
                
            }
            
            var fifteenMinutesAgo = DateTime.Today.AddMinutes(-15);
            
            transactions = _transactionService.GetAll().Where(e => e.CreateDate >= fifteenMinutesAgo);
            
            if (transactions.Count() >= 100)
            {
                
                throw new InvalidOperationException(
                    "Cannot query Strava API: Over 100 transactions were made in the last 15 minutes");
                
            }

        }
        
    }
}