﻿using API.Extensions;
using Application;
using Database;
using FeedApp.Extensions;
using System.Text.Json.Serialization;
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
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000")
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

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
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

            if (env.IsDevelopment()) app.UseCors("AllowAll");
            app.UseAuthorization();

            app.UseEndpoints(e =>
            {
                e.MapControllers();
                e.MapFallbackToFile("index.html");
            });
        }
    }

}
