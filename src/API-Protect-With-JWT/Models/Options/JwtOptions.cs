namespace API_Protect_With_JWT.Models.Options
{
    public class JwtOptions
    {
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public string Secret { get; set; }

        public int Expires { get; set; }
    }
}