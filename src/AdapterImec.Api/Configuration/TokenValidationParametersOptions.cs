namespace AdapterImec.Api.Configuration
{
    /// <summary>
    /// Child of JwtBearerOptions
    /// </summary>
    public class TokenValidationParametersOptions
    {
        /// <summary>
        /// ValidateAudience
        /// </summary>
        public bool ValidateAudience { get; set; }

        /// <summary>
        /// ValidateIssuer
        /// </summary>
        public bool ValidateIssuer { get; set; }

        /// <summary>
        /// ValidIssuers
        /// </summary>
        public string[] ValidIssuers { get; set; }
    }
}
