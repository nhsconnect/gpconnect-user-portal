using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Request;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Builders.Interfaces
{
    public interface IInteractionsBuilder
    {
        GpConnectInteractions Build(List<GpConnectInteractionForSupplier> interactions);
    }
}