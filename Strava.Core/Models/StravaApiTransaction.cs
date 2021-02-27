using System;

namespace Strava.Core.Models
{
    public class StravaApiTransaction
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Resouce { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
    }
}