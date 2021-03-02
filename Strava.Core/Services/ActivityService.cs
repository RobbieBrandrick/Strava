using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Strava.Core.Data;
using Strava.Core.Dto;
using Strava.Core.Extensions;
using Strava.Core.Models;

namespace Strava.Core.Services
{
    public interface IActivityService
    {
        Task<IEnumerable<Activity>> GetAll(bool refreshCache = false);
        IQueryable<Activity> GetAllFromCache();
    }

    public class ActivityService : IActivityService
    {
        private readonly StravaDbContext _dbContext;
        private readonly IActivityApiService _activityApiService;

        public ActivityService(StravaDbContext dbContext, IActivityApiService activityApiService)
        {
            _dbContext = dbContext;
            _activityApiService = activityApiService;
        }

        /// <summary>
        /// Retrieves all activities getting the latest from the strava api, and the others from cache 
        /// </summary>
        /// <param name="refreshCache">Refresh the cache from strava api</param>
        /// <returns></returns>
        public async Task<IEnumerable<Activity>> GetAll(bool refreshCache = false)
        {
            
            await CacheActivities(refreshCache);

            IQueryable<Activity> activities = GetAllFromCache();

            return activities;
            
        }

        /// <summary>
        /// Retrieves all activities from the database cache
        /// </summary>
        public IQueryable<Activity> GetAllFromCache()
        {
            
            return _dbContext.Activities;
            
        }      
        
        /// <summary>
        /// Add new activities to the database
        /// </summary>
        private async Task Add(List<Activity> activities)
        {

            var existingIds = _dbContext.Activities.Select(e => e.Id);

            activities = activities.Where(e => !existingIds.Contains(e.Id)).ToList();
            
            foreach (var activity in activities)
            {
                _dbContext.Entry(activity).State =
                    existingIds.Contains(activity.Id) ? EntityState.Modified : EntityState.Added;
            }
            
            await _dbContext.SaveChangesAsync();
            
        }        

        private async Task CacheActivities(bool refreshCache)
        {
            
            DateTime? after = await GetLatestStartDate();

            if (refreshCache)
            {
                after = null;
            }

            List<ActivityDto> activityDtos = await _activityApiService.GetActivities(null, after);
            
            List<Activity> activities = activityDtos.ToDomain();
            
            await Add(activities);
            
        }

        private async Task<DateTime?> GetLatestStartDate()
        {
            
            Activity latest = await GetLatest();
            
            DateTime? after = latest?.LocalDate;
            
            return after;
            
        }
        
        private async Task<Activity> GetLatest()
        {
            
            Activity result = await GetAllFromCache()
                .OrderByDescending(e => e.LocalDate)
                .FirstOrDefaultAsync();

            return result;
            
        }

    }
}