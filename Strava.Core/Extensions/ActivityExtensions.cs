using System.Collections.Generic;
using System.Linq;
using Strava.Core.Dto;
using Strava.Core.Models;

namespace Strava.Core.Extensions
{
    public static class ActivityExtensions
    {
        public static List<Activity> ToDomain(this List<ActivityDto> activityDtos)
        {
            List<Activity> activities = activityDtos
                .Select(e => new Activity()
                {
                    Id = e.id,
                    Type = e.type,
                    Name = e.name,
                    LocalDate = e.start_date_local,
                    UTCDate = e.start_date,
                    Timezone = e.timezone,
                    MovingTime = e.moving_time,
                    ElapsedTime = e.elapsed_time,
                    Distance = e.distance,
                    AverageSpeed = e.average_speed,
                    MaxSpeed = e.max_speed,
                    ElevationGain = e.total_elevation_gain,
                    ElevationLow = e.elev_low,
                    ElevationHigh = e.elev_high
                })
                .ToList();

            return activities;
            
        }

        public static IList<IList<object>> ToGoogleSheetObjects(this List<Activity> activities, bool includeHeader = true)
        {
            
            IList<IList<object>> values = new List<IList<object>>();

            if (includeHeader)
            {
                AddActivityGoogleSheetHeaders(values);
            }
            
            foreach (var activity in activities)
            {

                IList<object> value = new List<object>()
                {
                    activity.Id,
                    activity.Type,
                    activity.Name,
                    activity.LocalDate,
                    activity.UTCDate,
                    activity.Timezone,
                    activity.MovingTime,
                    activity.ElapsedTime,
                    activity.Distance,
                    activity.AverageSpeed,
                    activity.MaxSpeed,
                    activity.ElevationGain,
                    activity.ElevationLow,
                    activity.ElevationHigh
                    
                };
                
                values.Add(value);

            }

            return values;

        }
        
        private static void AddActivityGoogleSheetHeaders(IList<IList<object>> values)
        {
            
            IList<object> value = new List<object>()
            {
                "Id",
                "Type",
                "Name",
                "Date",
                "UTC Date",
                "Timezone",
                "Moving Time",
                "Elapsed Time",
                "Distance",
                "Average Speed",
                "Max Speed",
                "Elevation Gain",
                "Elevation Low",
                "Elevation High",
            };
            
            values.Add(value);
            
        }        
    }
}