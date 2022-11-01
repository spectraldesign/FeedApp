using API.Extensions;
using Application;
using Database;
using FeedApp.Extensions;
namespace FeedApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure(Configuration);

            services.AddApplication();

            services.AddHealthChecks();

            services.AddControllers();

            services.AddSwagger();

            services.ConfigureIdentity();

            services.ConfigureJWT(Configuration);

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/api/Error");
                app.UseHsts();
            }
            app.UseHealthChecks("/api/health");

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FeedApp");
                c.InjectStylesheet("/swagger-ui/SwaggerDark.css");
            });
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            if (env.IsDevelopment()) app.UseCors("DangerZone");
            app.UseAuthorization();

            app.UseEndpoints(e =>
            {
                e.MapControllers();
                e.MapFallbackToFile("index.html");
            });
        }
    }

}
