namespace Strava.Core.Models
{
    public class StravaClientSettings
    {
        public const string ConfigSection = "StravaClientSettings";
        public string Code { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}