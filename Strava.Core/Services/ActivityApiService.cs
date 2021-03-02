using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using Strava.Core.Dto;

namespace Strava.Core.Services
{
    public interface IActivityApiService
    {
        Task<List<ActivityDto>> GetActivities(int pageSize = 30, int page = 1, DateTime? before = null,
            DateTime? after = null);

        Task<List<ActivityDto>> GetActivities(DateTime? before, DateTime? after);
    }

    public class ActivityApiService : IActivityApiService
    {
        private readonly IStravaClientService _stravaClientService;

        public ActivityApiService(IStravaClientService stravaClientService)
        {
            _stravaClientService = stravaClientService;
        }

        public async Task<List<ActivityDto>> GetActivities(int pageSize = 30, int page = 1, DateTime? before = null,
            DateTime? after = null)
        {
            var request = new RestRequest("api/v3/athlete/activities");

            request.AddQueryParameter("per_page", pageSize.ToString());
            request.AddQueryParameter("page", page.ToString());
            if (before != null)
                request.AddQueryParameter("before", new DateTimeOffset(before.Value).ToUnixTimeSeconds().ToString());
            if (after != null)
                request.AddQueryParameter("after", new DateTimeOffset(after.Value).ToUnixTimeSeconds().ToString());

            List<ActivityDto> activities = await _stravaClientService.Execute<List<ActivityDto>>(request);

            if (activities.Any(e => string.IsNullOrWhiteSpace(e.id) || e.id == "0"))
            {
                
                throw new InvalidOperationException("Activity API Error: Activity Id is zero. Likely, too many requests were made");
                
            }

            return activities;
        }

        public async Task<List<ActivityDto>> GetActivities(DateTime? before = null, DateTime? after = null)
        {
            List<ActivityDto> activityDtos = new List<ActivityDto>();
            List<ActivityDto> response;
            var pageIndex = 1;

            do
            {
                response = await GetActivities(100, pageIndex++, after: after);

                activityDtos.AddRange(response);
            } while (response.Any());

            return activityDtos;
        }
    }
}