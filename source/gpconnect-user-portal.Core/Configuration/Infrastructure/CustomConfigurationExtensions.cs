using gpconnect_user_portal.Core.Configuration.Mapping;
using Microsoft.Extensions.Configuration;
using System;

namespace gpconnect_user_portal.Core.Configuration
{
    public static class CustomConfigurationExtensions
    {
        public static IConfigurationBuilder AddConfiguration(this IConfigurationBuilder configuration, Action<ConfigurationOptions> options)
        {
            _ = options ?? throw new ArgumentNullException(nameof(options));
            var myConfigurationOptions = new ConfigurationOptions();
            options(myConfigurationOptions);
            configuration.Add(new ConfigurationSource(myConfigurationOptions));
            return configuration;
        }
    }

    public class ConfigurationSource : IConfigurationSource
    {
        public string ConnectionString { get; set; }

        public ConfigurationSource(ConfigurationOptions options)
        {
            ConnectionString = options.ConnectionString;

        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new CustomConfigurationProvider(this);
        }
    }

    public class ConfigurationOptions
    {
        public string ConnectionString { get; set; }
    }

    public class CustomConfigurationProvider : ConfigurationProvider
    {
        public ConfigurationSource Source { get; }

        public CustomConfigurationProvider(ConfigurationSource source)
        {
            Source = source;            
        }

        public override void Load()
        {
            MappingExtensions.ConfigureMappingServices();
        }
    }
}
