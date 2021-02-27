using System.Threading.Tasks;
using RestSharp;
using Strava.Core.Dto;

namespace Strava.Core.Services
{
    public interface IAthleteApiService
    {
        Task<AthleteDto> GetAthlete();
    }

    public class AthleteApiService : IAthleteApiService
    {
        private readonly IStravaClientService _stravaClientService;

        public AthleteApiService(IStravaClientService stravaClientService)
        {
            _stravaClientService = stravaClientService;
        }

        public async Task<AthleteDto> GetAthlete()
        {
            
            AthleteDto athlete = await _stravaClientService.Execute<AthleteDto>(new RestRequest("api/v3/athlete"));

            return athlete;

        }
        
        
    }
}