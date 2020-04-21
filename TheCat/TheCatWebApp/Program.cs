using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using TheCatAPIIntegration.Service;
using TheCatDomain.Models;
using TheCatAPIIntegration.LogExtensions;

namespace TheCatWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var appConfiguration = new AppConfiguration();
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddELKLogProvider(new ELKIntegrationService(appConfiguration));
                });
        }
    }
}
