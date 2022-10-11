namespace PsylabsCase.Core.Options
{
    public class JwtOptions
    {
        public string SigningKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiresInMin { get; set; }
    }
}
