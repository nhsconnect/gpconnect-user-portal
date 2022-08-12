using GpConnect.NationalDataSharingPortal.EndUserPortal.Builders.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Request;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Builders;

public class SignatoryBuilder : ISignatoryBuilder
{
    public SignatoryDetails Build(string signatoryName, string signatoryEmail, string signatoryPosition)
    {
        return new SignatoryDetails
        {
            Name = signatoryName,
            Email = signatoryEmail,
            Position = signatoryPosition
        };
    }
}
