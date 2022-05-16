﻿using gpconnect_user_portal.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace gpconnect_user_portal.Core.Configuration.Infrastructure
{
    public static class CustomConfigurationBuilder
    {
        public static void AddCustomConfiguration(HostBuilderContext context, IConfigurationBuilder builder)
        {
            builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            builder.AddEnvironmentVariables();
            var configuration = builder.Build();

            // builder.AddConfiguration(options =>
            // {
                // options.ConnectionString = configuration.GetConnectionString(ConnectionStrings.DefaultConnection);
            // });
        }
    }
}
