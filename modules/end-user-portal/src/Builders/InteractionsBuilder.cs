using GpConnect.NationalDataSharingPortal.EndUserPortal.Builders.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Request;
using static GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Constants.GpConnectInteractions;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Builders;

public class InteractionsBuilder : IInteractionsBuilder
{
    public GpConnectInteractions Build(List<Helpers.Constants.GpConnectInteractions> interactions)
    {
        return new GpConnectInteractions
        {
            AccessRecordHTMLEnabled = interactions.Contains(AccessRecordHTML),
            AppointmentManagementEnabled = interactions.Contains(AppointmentManagement),
            StructuredRecordEnabled = interactions.Contains(AccessRecordStructured),
            SendDocumentEnabled = interactions.Contains(SendDocument)
        };
    }
}
