using Dapper.FluentMap;
using gpconnect_user_portal.DAL.Mapping;
using gpconnect_user_portal.DAL.Mappings;

namespace gpconnect_user_portal.Core.Configuration.Mapping
{
    public static class MappingExtensions
    {
        public static void ConfigureMappingServices()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new SearchResultMap());
                config.AddMap(new GeneralConfigurationMap());
                config.AddMap(new ReferenceApiQueryMap());
                config.AddMap(new ReferenceConfigurationMap());
                config.AddMap(new ReferenceOrganisationMap());
            });
        }
    }
}
