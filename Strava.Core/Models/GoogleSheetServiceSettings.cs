namespace Strava.Core.Models
{
    public class GoogleSheetServiceSettings
    {
        public const string ConfigSection = "GoogleSheetService";
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ApplicationName { get; set; }
        public string CredentialLocation { get; set; }
    }
}