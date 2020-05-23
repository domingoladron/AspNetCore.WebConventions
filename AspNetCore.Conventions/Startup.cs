using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace AspNetCore.WebConventions
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
            services.AddOpenApiDocument(
               config =>
               {
                   config.PostProcess = document =>
                   {
                       document.Info.Version = "v1";
                       document.Info.Title = "Web Conventions Example API";
                       document.Info.Description = "ASP.NET Web Conventions Example API";
                       document.Info.TermsOfService = "None";
                       document.Info.Contact = new NSwag.OpenApiContact
                       {
                           Name = "Chris Laine",
                           Url = "https://medium.com/@domingoladron"
                       };
                       document.Info.License = new NSwag.OpenApiLicense
                       {
                           Name = "Use under LICX",
                           Url = "https://example.com/license"
                       };
                   };
               }
           );

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseReDoc(config =>
            {
                config.Path = "/redoc";
            }
                );
        }
    }
}
