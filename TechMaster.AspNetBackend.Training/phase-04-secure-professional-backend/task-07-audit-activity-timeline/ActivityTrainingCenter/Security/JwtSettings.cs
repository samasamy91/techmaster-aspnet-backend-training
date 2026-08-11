namespace TrainingCenterAuthTask01.Security
{
    public class JwtSettings
    {
        public const string SectionName = "Jwt";
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpirationHours { get; set; }
    }
}
