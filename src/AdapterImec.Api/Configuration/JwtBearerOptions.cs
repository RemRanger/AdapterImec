namespace AdapterImec.Api.Configuration
{
    public class JwtBearerOptions
    {
        /// <summary>
        /// Data for TokenValidationParameters
        /// </summary>
        public TokenValidationParametersOptions TokenValidationParameters { get; set; }

        /// <summary>
        /// Url to Authority
        /// </summary>
        public string Authority { get; set; }
    }
}
