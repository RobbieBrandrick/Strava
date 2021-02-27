namespace Strava.Core.Dto
{
    public class TokenDto
    {
        public string token_type { get; set; }
        public int expires_at { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string access_token { get; set; }
        public AthleteDto athlete { get; set; }
    }
}