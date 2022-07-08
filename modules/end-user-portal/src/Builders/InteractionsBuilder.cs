using GpConnect.NationalDataSharingPortal.EndUserPortal.Builders.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Request;
using static GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Constants.GpConnectInteractions;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Builders
{
    public class InteractionsBuilder: IInteractionsBuilder
    {
        public GpConnectInteractions Build(List<GpConnectInteractionForSupplier> interactions)
        {
            return new GpConnectInteractions 
            {
                AccessRecordHTMLEnabled = IsInteractionEnabled(AccessRecordHTML, interactions),
                AppointmentManagementEnabled = IsInteractionEnabled(AppointmentManagement, interactions),
                SendDocumentEnabled = IsInteractionEnabled(SendDocument, interactions),
                StructuredRecordEnabled = IsInteractionEnabled(AccessRecordStructured, interactions)
            };
        }

        private bool IsInteractionEnabled(string interactionName, List<GpConnectInteractionForSupplier> interactions)
        {
            var interaction = interactions.Find(x => x.GpConnectInteractionForSupplierValue == interactionName);

            return interaction != null && interaction.Selected;
        }
    }
}