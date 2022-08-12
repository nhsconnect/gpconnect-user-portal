using GpConnect.NationalDataSharingPortal.EndUserPortal.Builders;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Constants;
using System.Collections.Generic;
using Xunit;

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
            var result = _sut.Build(new List<GpConnectInteractions>());

            Assert.False(result.AccessRecordHTMLEnabled);
            Assert.False(result.StructuredRecordEnabled);
            Assert.False(result.AppointmentManagementEnabled);
            Assert.False(result.SendDocumentEnabled);
        }

        [Theory]
        [InlineData(GpConnectInteractions.AccessRecordHTML, true, false, false, false)]
        [InlineData(GpConnectInteractions.AccessRecordStructured, false, true, false, false)]
        [InlineData(GpConnectInteractions.AppointmentManagement, false, false, true, false)]
        [InlineData(GpConnectInteractions.SendDocument, false, false, false, true)]
        public void Build_GivenSingleEnabledRecord_ReturnsExpectedObject(GpConnectInteractions value, bool expectedAccessRecordHtml, bool expectedAccessRecordStructured, bool expectedAppointmentManagement, bool expectedSendDocument)
        {
            var result = _sut.Build(new List<GpConnectInteractions> { value });

            Assert.StrictEqual(expectedAccessRecordHtml, result.AccessRecordHTMLEnabled);
            Assert.StrictEqual(expectedAccessRecordStructured, result.StructuredRecordEnabled);
            Assert.StrictEqual(expectedAppointmentManagement, result.AppointmentManagementEnabled);
            Assert.StrictEqual(expectedSendDocument, result.SendDocumentEnabled);
        }

        [Fact]
        public void Build_GivenAllDisabledRecords_ReturnsObjectWithNoInteractionsEnabled()
        {
            var result = _sut.Build(new List<EndUserPortal.Helpers.Constants.GpConnectInteractions>());

            Assert.False(result.AccessRecordHTMLEnabled);
            Assert.False(result.StructuredRecordEnabled);
            Assert.False(result.AppointmentManagementEnabled);
            Assert.False(result.SendDocumentEnabled);
        }

        [Fact]
        public void Build_GivenSomeEnabledRecords_ReturnsObjectWithSomeInteractionsEnabled()
        {
            var result = _sut.Build(new List<GpConnectInteractions> {
                GpConnectInteractions.AccessRecordHTML,
                GpConnectInteractions.AppointmentManagement
            });

            Assert.True(result.AccessRecordHTMLEnabled);
            Assert.False(result.StructuredRecordEnabled);
            Assert.True(result.AppointmentManagementEnabled);
            Assert.False(result.SendDocumentEnabled);
        }

        [Fact]
        public void Build_GivenAllEnabledRecords_ReturnsObjectWithAllInteractionsEnabled()
        {
            var result = _sut.Build(new List<GpConnectInteractions> {
                GpConnectInteractions.AccessRecordHTML,
                GpConnectInteractions.AppointmentManagement,
                GpConnectInteractions.AccessRecordStructured,
                GpConnectInteractions.SendDocument
            });

            Assert.True(result.AccessRecordHTMLEnabled);
            Assert.True(result.StructuredRecordEnabled);
            Assert.True(result.AppointmentManagementEnabled);
            Assert.True(result.SendDocumentEnabled);
        }
    }
}
