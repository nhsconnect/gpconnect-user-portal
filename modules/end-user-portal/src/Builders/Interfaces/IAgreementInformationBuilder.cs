using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Request;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Response;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Builders.Interfaces;

public interface IAgreementInformationBuilder
{
    public Task<AgreementInformation> Build(string organisation, string supplier, string UseCase, List<int> interactions, string SignatoryName, string SignatoryEmail, string SignatoryPosition);
}   

