using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Strava.Core.Models;
using Strava.Core.Services;

namespace Strava.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;

        public ActivityController(IActivityService activityService)
        {
            
            _activityService = activityService;
            
        }

        [HttpGet]
        public async Task<List<Activity>> GetActivities()
        {
            
            List<Activity> activities = await _activityService
                .GetAllFromCache()
                .OrderByDescending(a => a.LocalDate)
                .ToListAsync();

            return activities;
            
        }
        
    }
}