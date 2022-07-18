using GpConnect.NationalDataSharingPortal.EndUserPortal.Builders.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Request;
using static GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Constants.GpConnectInteractions;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Builders
{
    public class InteractionsBuilder: IInteractionsBuilder
    {
        public GpConnectInteractions Build(List<int> interactions)
        {
            return new GpConnectInteractions 
            {
                AccessRecordHTMLEnabled = IsInteractionEnabled(AccessRecordHTML, interactions),
                AppointmentManagementEnabled = IsInteractionEnabled(AppointmentManagement, interactions),
                StructuredRecordEnabled = IsInteractionEnabled(AccessRecordStructured, interactions),
                SendDocumentEnabled = IsInteractionEnabled(SendDocument, interactions)
            };
        }

        private bool IsInteractionEnabled(Helpers.Constants.GpConnectInteractions gpConnectInteraction, List<int> interactions)
        {
            return interactions.Contains((int)gpConnectInteraction);
        }
    }
}
