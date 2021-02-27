using System;

namespace Strava.Core.Models
{
    public class Activity
    {
        public string Id {get; set;}
        public string Type {get; set;}
        public string Name {get; set;}
        public DateTime LocalDate {get; set;}
        public DateTime UTCDate {get; set;}
        public string Timezone {get; set;}
        public int? MovingTime {get; set;}
        public int? ElapsedTime {get; set;}
        public double? Distance {get; set;}
        public double? AverageSpeed {get; set;}
        public double? MaxSpeed {get; set;}
        public double? ElevationGain {get; set;}
        public double? ElevationHigh {get; set;}
        public double? ElevationLow {get; set;}
    }
}