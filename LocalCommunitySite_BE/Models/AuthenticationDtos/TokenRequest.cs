namespace LocalCommunitySite.API.Models.AuthenticationDtos
{
    public class TokenRequest
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}
