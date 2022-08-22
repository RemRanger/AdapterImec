using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AdapterImec.Api.Configuration
{
    public static class AddKeyCloakAuthorization
    {
        public static void AddKeyCloakAuthorizion(this IServiceCollection services, IConfiguration configuration)
        {
            // load config items
            var bearerOptions = new Configuration.JwtBearerOptions();
            configuration.GetSection("JwtBearerOptions").Bind(bearerOptions);

            var jwtBearerEvents = CreateBearerEvents();

            services.AddAuthorization();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(opt =>
                        {
                            opt.TokenValidationParameters = new TokenValidationParameters()
                            {
                                ValidateAudience = bearerOptions.TokenValidationParameters.ValidateAudience,
                                ValidateIssuer = bearerOptions.TokenValidationParameters.ValidateIssuer,
                                ValidIssuers = bearerOptions.TokenValidationParameters.ValidIssuers
                            };
                            opt.Authority = bearerOptions.Authority;
                            opt.RequireHttpsMetadata = false;
                            opt.Events = jwtBearerEvents;
                        });

        }

        private static JwtBearerEvents CreateBearerEvents()
        {
            return new JwtBearerEvents
            {
                OnTokenValidated = context =>
                {
                    // Add the access_token as a claim, as we may actually need it
                    ClaimsIdentity claimsIdentity = (ClaimsIdentity)context.Principal.Identity;

                    // flatten realm_access because Microsoft identity model doesn't support nested claims
                    // by map it to Microsoft identity model, because automatic JWT bearer token mapping already processed here
                    if (claimsIdentity.IsAuthenticated && claimsIdentity.HasClaim((claim) => claim.Type == "realm_access"))
                    {
                        var realmAccessClaim = claimsIdentity.FindFirst((claim) => claim.Type == "realm_access");
                        var realmAccessAsDict = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(realmAccessClaim.Value);
                        if (realmAccessAsDict["roles"] != null)
                        {
                            foreach (var role in realmAccessAsDict["roles"])
                            {
                                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
                            }
                        }
                    }

                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = context =>
                {
                    var loggerFactory = context.HttpContext.RequestServices.GetService<ILoggerFactory>();
                    var logger = loggerFactory.CreateLogger(nameof(AddKeyCloakAuthorization));
                    logger.LogInformation($"Authentication Failed: {context.Exception}");

                    return Task.CompletedTask;
                }
            };
        }
    }
}
