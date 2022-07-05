using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using GpConnect.NationalDataSharingPortal.Api.Dal.Extensions;
using GpConnect.NationalDataSharingPortal.Api.Dal.Interfaces;
using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;

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
        
        var siteAttributes = new Dictionary<string,string> {
            { "OdsCode", acceptanceInformation.Organisation.OdsCode },
            { "SiteName", acceptanceInformation.Organisation.Name },
            { "SiteAddressLine1", acceptanceInformation.Organisation.AddressLine1 },
            { "SiteAddressLine2", acceptanceInformation.Organisation.AddressLine2 },
            { "SiteAddressTown", acceptanceInformation.Organisation.Town },
            { "SiteAddressCounty", acceptanceInformation.Organisation.County },
            { "SiteAddressCountry", acceptanceInformation.Organisation.Country },
            { "SitePostcode", acceptanceInformation.Organisation.PostCode },
            { "UseCaseDescription", acceptanceInformation.UseCase },
            { "IsStructuredEnabled", acceptanceInformation.Interactions.StructuredRecordEnabled.ToDbString() },
            { "IsAppointmentEnabled", acceptanceInformation.Interactions.AppointmentManagementEnabled.ToDbString() },
            { "IsHtmlEnabled", acceptanceInformation.Interactions.AccessRecordHTMLEnabled.ToDbString() },
            { "IsSendDocumentEnabled", acceptanceInformation.Interactions.SendDocumentEnabled.ToDbString() },
            { "SoftwareSupplierName", acceptanceInformation.SoftwareSupplierName },
            { "SignatoryName", acceptanceInformation.Signatory.Name },
            { "SignatoryEmail", acceptanceInformation.Signatory.Email },
            { "SignatoryPosition", acceptanceInformation.Signatory.Position },
        };

        await Task.WhenAll(siteAttributes.Select(item => {
            if (string.IsNullOrWhiteSpace(item.Value)) 
            {
                return Task.CompletedTask;
            }
            return _siteService.CreateSiteAttributeAsync(siteDefinition.UniqueId, item.Key, item.Value);
        }));
    }
}