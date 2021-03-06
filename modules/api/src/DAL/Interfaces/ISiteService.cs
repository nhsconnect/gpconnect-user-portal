using System;
using System.Threading.Tasks;
using GpConnect.NationalDataSharingPortal.Api.Dal.Models;

namespace GpConnect.NationalDataSharingPortal.Api.Dal.Interfaces;

public interface ISiteService
{
    public Task<SiteDefinition> CreateSiteDefinitionAsync(string odsCode);
    
    Task CreateSiteAttributesAsync(Guid uniqueId, string site_attribute_data);
}