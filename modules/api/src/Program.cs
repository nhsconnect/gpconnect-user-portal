using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace gpconnect_user_portal.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webHostDefaultsBuilder =>
                {                    
                    webHostDefaultsBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration((context, builder) => {
                    builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    builder.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);

                    // More config here
                });
        }
    }
}
