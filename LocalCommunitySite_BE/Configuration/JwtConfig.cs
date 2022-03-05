namespace LocalCommunitySite.API
{
    public class JwtConfig
    {
        public JwtConfig()
        {
        }

        public JwtConfig(string secret)
        {
            Secret = secret;
        }

        public string Secret { get; set; }
    }
}
