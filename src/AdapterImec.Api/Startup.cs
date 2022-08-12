using AdapterImec.Api.Configuration;
using AdapterImec.Api.Filters;
using AdapterImec.Application;
using AdapterImec.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AdapterImec.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers(options =>
            {
                //var xml = new XmlSerializerOutputFormatter();
                //xml.WriterSettings.OmitXmlDeclaration
                //options.OutputFormatters.Add();
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));

            })
            .AddXmlSerializerFormatters()
            ;

            services.AddScoped<Middleware.LoggingMiddleware>();
            services.AddApplication();
            services.AddRepositories(this.Configuration);
            services.AddSwagger(this.Configuration.GetValue<string>($"{nameof(JwtBearerOptions)}:{nameof(JwtBearerOptions.Authority)}"));
            services.AddHealthChecks();

            services.AddKeyCloakAuthorizion(this.Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDatabaseMigrator databaseMigrator)
        {
            databaseMigrator.Init();

            const string usePathBase = "/adapter-imec";
            app.UsePathBase(usePathBase);

            if (env.IsDevelopment())
            {
                //Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
                app.UseDeveloperExceptionPage();
            }

            app.UseCustomSwagger(usePathBase);
            app.UseMiddleware<Middleware.LoggingMiddleware>();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints
                    .MapHealthChecks("/health/ready", new HealthCheckOptions
                    {
                        Predicate = healthCheck => healthCheck.Tags.Contains("ready")
                    })
                    .AllowAnonymous()
                    ;

                endpoints
                    .MapHealthChecks("/health/live", new HealthCheckOptions
                    {
                        Predicate = _ => false
                    })
                    .AllowAnonymous()
                    ;

                endpoints.MapControllers();
            });
        }
    }
}
