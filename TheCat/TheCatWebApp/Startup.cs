using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using TheCatAPIIntegration.Service;
using TheCatApplication.Commands;
using TheCatDomain.Interfaces;
using TheCatDomain.Interfaces.Application;
using TheCatDomain.Interfaces.Integration;
using TheCatDomain.Interfaces.Repositories;
using TheCatDomain.Models;
using TheCatRepository.Context;
using TheCatRepository.Repositories;
using TheCatWebApp.Data;

namespace TheCatWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddCors();
            services.AddControllers();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "TheCatWebApp",
                    Version = "v1",
                    Description = "API para expor informações coletadas da API de Gatos: thecatapi.com.",
                    TermsOfService = new Uri("https://thecatapi.com/"),
                    Contact = new OpenApiContact
                    {
                        Name = "Jefferson Bompadre",
                        Email = "jefferson.bompadre@hotmail.com",
                        Url = new Uri("https://github.com/jeffersonbompadre"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://github.com/jeffersonbompadre/thecatapi"),
                    }
                });
            });

            // Injeção de Dependência
            services.AddSingleton<WeatherForecastService>();

            services.AddScoped<IAppConfiguration, AppConfiguration>();
            services.AddScoped<TheCatDBContext>();
            services.AddScoped<IBreedsRepository, BreedsRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IImageUrlRepository, ImageUrlRepository>();
            services.AddScoped<ITheCatAPI, TheCatAPIService>();
            services.AddScoped<ICommandCapture, CommandCapture>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blazor Server");
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(x =>
                x.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
