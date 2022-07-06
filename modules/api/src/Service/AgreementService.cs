using GpConnect.NationalDataSharingPortal.Api.Dal.Interfaces;
using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GpConnect.NationalDataSharingPortal.Api.Service;

public class AgreementService: IAgreementService
{
    private readonly ISiteService _siteService;

    public AgreementService(ISiteService siteService)
    {
        _siteService = siteService;
    }

    public async Task CreateAgreementAsync(AgreementInformation acceptanceInformation)
    {
        var siteDefinition = await _siteService.CreateSiteDefinitionAsync(acceptanceInformation.Organisation.OdsCode);

        var siteAttributes = new List<SiteAttributeAddRequest> {
            new SiteAttributeAddRequest { Name = "OdsCode", Value = acceptanceInformation.Organisation.OdsCode },
            new SiteAttributeAddRequest { Name = "SiteName", Value = acceptanceInformation.Organisation.Name },
            new SiteAttributeAddRequest { Name = "SiteAddressLine1", Value = acceptanceInformation.Organisation.AddressLine1 },
            new SiteAttributeAddRequest { Name = "SiteAddressLine2", Value = acceptanceInformation.Organisation.AddressLine2 },
            new SiteAttributeAddRequest { Name = "SiteAddressTown", Value = acceptanceInformation.Organisation.Town },
            new SiteAttributeAddRequest { Name = "SiteAddressCounty", Value = acceptanceInformation.Organisation.County },
            new SiteAttributeAddRequest { Name = "SiteAddressCountry", Value = acceptanceInformation.Organisation.Country },
            new SiteAttributeAddRequest { Name = "SitePostcode", Value = acceptanceInformation.Organisation.PostCode },
            new SiteAttributeAddRequest { Name = "UseCaseDescription", Value = acceptanceInformation.UseCase },
            new SiteAttributeAddRequest { Name = "IsStructuredEnabled", Value = acceptanceInformation.Interactions.StructuredRecordEnabled.ToString() },
            new SiteAttributeAddRequest { Name = "IsAppointmentEnabled", Value = acceptanceInformation.Interactions.AppointmentManagementEnabled.ToString() },
            new SiteAttributeAddRequest { Name = "IsSendDocumentEnabled", Value = acceptanceInformation.Interactions.SendDocumentEnabled.ToString() },
            new SiteAttributeAddRequest { Name = "IsHtmlEnabled", Value = acceptanceInformation.Interactions.AccessRecordHTMLEnabled.ToString() },
            new SiteAttributeAddRequest { Name = "SoftwareSupplierName", Value = acceptanceInformation.SoftwareSupplierName },
            new SiteAttributeAddRequest { Name = "SignatoryName", Value = acceptanceInformation.Signatory.Name },
            new SiteAttributeAddRequest { Name = "SignatoryEmail", Value = acceptanceInformation.Signatory.Email },
            new SiteAttributeAddRequest { Name = "SignatoryPosition", Value = acceptanceInformation.Signatory.Position }
        };       

        var site_attribute_data = JsonConvert.SerializeObject(siteAttributes);
        await _siteService.CreateSiteAttributesAsync(siteDefinition.UniqueId, site_attribute_data);
    }
}



