using Dapper.FluentMap;
using gpconnect_user_portal.DAL.Mappings;

namespace gpconnect_user_portal.Framework.Configuration.Mapping
{
    public static class MappingExtensions
    {
        public static void ConfigureMappingServices()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new SearchResultMap());
            });
        }
    }
}
