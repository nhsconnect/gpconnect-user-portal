using GpConnect.NationalDataSharingPortal.EndUserPortal.Builders;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using static GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Constants.GpConnectInteractions;
using Xunit;
using System.Collections.Generic;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test.Builders
{
    public class InteractionsBuilderTest
    {
        private InteractionsBuilder _sut;

        public InteractionsBuilderTest()
        {
            _sut = new InteractionsBuilder();
        }

        [Fact]
        public void Build_GivenEmptyList_ReturnsObjectWithNoInteractionsEnabled()
        {
            var result = _sut.Build(new List<GpConnectInteractionForSupplier>());

            Assert.False(result.AccessRecordHTMLEnabled);
            Assert.False(result.StructuredRecordEnabled);
            Assert.False(result.AppointmentManagementEnabled);
            Assert.False(result.SendDocumentEnabled);
        }

        [Theory]
        [InlineData(AccessRecordHTML, true, false, false, false)]
        [InlineData(AccessRecordStructured, false, true, false, false)]
        [InlineData(AppointmentManagement, false, false, true, false)]
        [InlineData(SendDocument, false, false, false, true)]
        public void Build_GivenSingleEnabledRecord_ReturnsExpectedObject(
            string value, 
            bool expectedAccessRecordHtml, 
            bool expectedAccessRecordStructured, 
            bool expectedAppointmentManagement, 
            bool expectedSendDocument)
        {
            var result = _sut.Build(new List<GpConnectInteractionForSupplier> { 
                new GpConnectInteractionForSupplier {
                    GpConnectInteractionForSupplierValue = value,
                    Selected = true
                }
            });

            Assert.StrictEqual(expectedAccessRecordHtml, result.AccessRecordHTMLEnabled);
            Assert.StrictEqual(expectedAccessRecordStructured, result.StructuredRecordEnabled);
            Assert.StrictEqual(expectedAppointmentManagement, result.AppointmentManagementEnabled);
            Assert.StrictEqual(expectedSendDocument, result.SendDocumentEnabled);
        }

        [Fact]
        public void Build_GivenAllDisabledRecords_ReturnsObjectWithNoInteractionsEnabled()
        {
            var result = _sut.Build(new List<GpConnectInteractionForSupplier> { 
                new GpConnectInteractionForSupplier {
                    GpConnectInteractionForSupplierValue = AccessRecordHTML,
                    Selected = false
                },
                new GpConnectInteractionForSupplier {
                    GpConnectInteractionForSupplierValue = AccessRecordStructured,
                    Selected = false
                },
                new GpConnectInteractionForSupplier {
                    GpConnectInteractionForSupplierValue = AppointmentManagement,
                    Selected = false
                },
                new GpConnectInteractionForSupplier {
                    GpConnectInteractionForSupplierValue = SendDocument,
                    Selected = false
                }
            });

            Assert.False(result.AccessRecordHTMLEnabled);
            Assert.False(result.StructuredRecordEnabled);
            Assert.False(result.AppointmentManagementEnabled);
            Assert.False(result.SendDocumentEnabled);
        }
    }
}