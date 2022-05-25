using Dapper.FluentMap;
using GpConnect.NationalDataSharingPortal.Api.Dal.Mapping;

namespace GpConnect.NationalDataSharingPortal.Api.Core.Mapping
{
    public static class MappingExtensions
    {
        public static void ConfigureMappingServices()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new CareSettingMap());
                config.AddMap(new CcgMap());
            });
        }
    }
}
