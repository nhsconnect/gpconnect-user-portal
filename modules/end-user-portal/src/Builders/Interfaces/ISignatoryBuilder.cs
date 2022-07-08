using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Request;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Builders.Interfaces
{
    public interface ISignatoryBuilder
    {
        SignatoryDetails Build(string signatoryName, string signatoryEmail, string signatoryPosition);
    }
}