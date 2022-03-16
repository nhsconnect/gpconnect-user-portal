using Dapper.FluentMap.Mapping;
using gpconnect_user_portal.DTO.Response.Configuration;

namespace gpconnect_user_portal.DAL.Mapping
{
    public class SpineConfigurationMap : EntityMap<Spine>
    {
        public SpineConfigurationMap()
        {
            Map(p => p.SpineFhirApiDirectoryServicesFqdn).ToColumn("spine_fhir_api_directory_services_fqdn");
            Map(p => p.SpineFhirApiSystemsRegisterFqdn).ToColumn("spine_fhir_api_systems_register_fqdn");
            Map(p => p.SpineFhirApiKey).ToColumn("spine_fhir_api_key");
        }
    }
}
