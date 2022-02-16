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
                config.AddMap(new SearchResultEntryMap());
                config.AddMap(new GeneralConfigurationMap());
                config.AddMap(new SpineConfigurationMap());
                config.AddMap(new ReferenceApiQueryMap());
                config.AddMap(new ReferenceConfigurationMap());
                config.AddMap(new ReferenceOrganisationMap());
                config.AddMap(new SsoConfigurationMap());
                config.AddMap(new ReferenceLookupMap());
                config.AddMap(new ReferenceLookupTypeMap());
                config.AddMap(new SiteDefinitionMap());
                config.AddMap(new SiteAttributeMap());
                config.AddMap(new EmailConfigurationMap());
                config.AddMap(new LoggingConfigurationMap());
                config.AddMap(new SupplierProductCapabilityMap());
                config.AddMap(new EmailMap());
                config.AddMap(new FhirApiQueryMap());
                config.AddMap(new EndpointChangeMap());
                config.AddMap(new EndpointChangeCountByStatusMap());
                config.AddMap(new LookupDataCountByTypeMap());
            });
        }
    }
}
