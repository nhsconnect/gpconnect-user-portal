using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Data.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Constants;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test.Pages.Apply;

public class SystemSupplierModelTest
{
    private readonly Mock<ISupplierService> _mockSupplierService;
    private readonly Mock<ITempDataProviderService> _mockTempDataProviderService;
    private readonly Mock<IOptions<ApplicationParameters>> _mockOptions;

    public SystemSupplierModelTest()
    {
        _mockSupplierService = new Mock<ISupplierService>();
        _mockTempDataProviderService = new Mock<ITempDataProviderService>();
        _mockOptions = new Mock<IOptions<ApplicationParameters>>();
        _mockSupplierService.Setup(mss => mss.GetSoftwareSuppliersAsync()).ReturnsAsync(new List<SoftwareSupplierResult>());
    }

    [Fact]
    public void OnGet_GetSoftwareSupplierNameList_ReturnsListOfSoftwareSuppliersFromTempData()
    {
        var softwareSupplierList = new List<SoftwareSupplierResult>
        {
            new SoftwareSupplierResult() { SoftwareSupplierId = 1, SoftwareSupplierName = "Software Supplier 1" },
            new SoftwareSupplierResult() { SoftwareSupplierId = 2, SoftwareSupplierName = "Software Supplier 2" },
            new SoftwareSupplierResult() { SoftwareSupplierId = 3, SoftwareSupplierName = "Software Supplier 3" }
        };

        var selectedSoftwareSupplier = new SoftwareSupplierResult () { SoftwareSupplierId = 1, SoftwareSupplierName = "Software Supplier 1" };

        _mockTempDataProviderService.Setup(mtd => mtd.PutItem(It.IsAny<string>(), softwareSupplierList));
        _mockTempDataProviderService.Setup(mtd => mtd.GetItem<List<SoftwareSupplierResult>>(It.IsAny<string>())).Returns(softwareSupplierList);
        _mockTempDataProviderService.Setup(mtd => mtd.GetItem<SoftwareSupplierResult>(It.IsAny<string>())).Returns(selectedSoftwareSupplier);

        var softwareSupplierModel = new SystemSupplierModel(_mockOptions.Object, _mockSupplierService.Object, _mockTempDataProviderService.Object)
        {
            SelectedSoftwareSupplierId = "1",
            DisplayGpConnectInteractionForSupplierList = true,
            GpConnectInteractionForSupplierList = new List<GpConnectInteractionForSupplier>() { new GpConnectInteractionForSupplier() { Id = 1, Selected = true, Value = "Software Supplier Name" } },
            SoftwareSupplierResultList = softwareSupplierList
        };

        Assert.Equal(2, softwareSupplierModel.SoftwareSupplierResultList[1].SoftwareSupplierId);
        Assert.Equal("Software Supplier 2", softwareSupplierModel.SoftwareSupplierResultList[1].SoftwareSupplierName);
        Assert.Equal("1", softwareSupplierModel.SelectedSoftwareSupplierId);
        Assert.False(softwareSupplierModel.IsSelectedSoftwareSupplier);
        Assert.Equal(3, softwareSupplierModel.SoftwareSupplierResultList.Count);
    }

    [Fact]
    public void OnGet_GetGpConnectInteractionForSupplierList_ReturnsListOfGpConnectInteractionForSupplierListFromTempData()
    {
        var gpConnectInteractionForSuppliers = new List<GpConnectInteractionForSupplier>
        {
            new GpConnectInteractionForSupplier() { Id = 1, Value = "Access Record: HTML", Selected = false },
            new GpConnectInteractionForSupplier() { Id = 2, Value = "Access Record: Structured", Selected = true },
            new GpConnectInteractionForSupplier() { Id = 3, Value = "Appointment Management", Selected = true },
            new GpConnectInteractionForSupplier() { Id = 4, Value = "Send Document", Selected = false }
        };

        _mockTempDataProviderService.Setup(mtd => mtd.PutItem(It.IsAny<string>(), gpConnectInteractionForSuppliers));
        _mockTempDataProviderService.Setup(mtd => mtd.GetItem<List<GpConnectInteractionForSupplier>>(It.IsAny<string>())).Returns(gpConnectInteractionForSuppliers);
        
        var softwareSupplierModel = new SystemSupplierModel(_mockOptions.Object, _mockSupplierService.Object, _mockTempDataProviderService.Object)
        {
            SelectedSoftwareSupplierId = "1",
            DisplayGpConnectInteractionForSupplierList = true,
            GpConnectInteractionForSupplierList = gpConnectInteractionForSuppliers
        };

        Assert.Equal(4, softwareSupplierModel.GpConnectInteractionForSupplierList.Count);
        Assert.Equal(1, softwareSupplierModel.GpConnectInteractionForSupplierList[0].Id);
        Assert.Equal("Access Record: Structured", softwareSupplierModel.GpConnectInteractionForSupplierList[1].Value);
        Assert.True(softwareSupplierModel.GpConnectInteractionForSupplierList[1].Selected);
    }

    [Fact]
    public async Task OnPost_IfValidEntry_RedirectsToNextPage()
    {
        var gpConnectInteractionForSuppliers = new List<GpConnectInteractionForSupplier>
        {
            new GpConnectInteractionForSupplier() { Id = 1, Value = "Access Record: HTML", Selected = false },
            new GpConnectInteractionForSupplier() { Id = 2, Value = "Access Record: Structured", Selected = true },
            new GpConnectInteractionForSupplier() { Id = 3, Value = "Appointment Management", Selected = true },
            new GpConnectInteractionForSupplier() { Id = 4, Value = "Send Document", Selected = false }
        };

        var softwareSupplierModel = new SystemSupplierModel(_mockOptions.Object, _mockSupplierService.Object, _mockTempDataProviderService.Object)
        {
            SelectedSoftwareSupplierId = "1",
            DisplayGpConnectInteractionForSupplierList = true,
            GpConnectInteractionForSupplierList = gpConnectInteractionForSuppliers,
            HasSelectedGpConnectInteractionForSupplier = true
        };

        var result = await softwareSupplierModel.OnPostNextAsync();

        Assert.IsType<RedirectToPageResult>(result);
    }

    [Fact]
    public async Task OnPost_IfTempDataHasNoData_RedirectsToTimeoutPage()
    {
        _mockTempDataProviderService.Setup(mtd => mtd.HasItems).Returns(false);
        var gpConnectInteractionForSuppliers = new List<GpConnectInteractionForSupplier>
        {
            new GpConnectInteractionForSupplier() { Id = 1, Value = "Access Record: HTML", Selected = false },
            new GpConnectInteractionForSupplier() { Id = 2, Value = "Access Record: Structured", Selected = true },
            new GpConnectInteractionForSupplier() { Id = 3, Value = "Appointment Management", Selected = true },
            new GpConnectInteractionForSupplier() { Id = 4, Value = "Send Document", Selected = false }
        };

        var softwareSupplierModel = new SystemSupplierModel(_mockOptions.Object, _mockSupplierService.Object, _mockTempDataProviderService.Object)
        {
            SelectedSoftwareSupplierId = "1",
            DisplayGpConnectInteractionForSupplierList = true,
            GpConnectInteractionForSupplierList = gpConnectInteractionForSuppliers,
            HasSelectedGpConnectInteractionForSupplier = true
        };

        var result = await softwareSupplierModel.OnPostNextAsync();
        Assert.IsType<RedirectToPageResult>(result);
        Assert.Contains("Organisation", ((RedirectToPageResult)result).PageName);
    }

    [Fact]
    public async Task OnPostCheckGpConnectInteractionForSupplierList_IfInvalidModel_ReturnValidationError()
    {
        var softwareSupplierModel = new SystemSupplierModel(_mockOptions.Object, _mockSupplierService.Object, _mockTempDataProviderService.Object);
        softwareSupplierModel.ModelState.AddModelError("SelectedSoftwareSupplier", "You must select a value for Software Supplier Name");

        var result = await softwareSupplierModel.OnPostCheckGpConnectInteractionForSupplierListAsync();

        Assert.StrictEqual(ModelValidationState.Invalid, softwareSupplierModel.ModelState.GetFieldValidationState("SelectedSoftwareSupplier"));
        Assert.IsType<PageResult>(result);
        Assert.True(softwareSupplierModel.ModelState.ErrorCount > 0);
    }

    [Fact]
    public async Task OnPost_TempDataPopulated_WithExpectedValues()
    {
        var gpConnectInteractionForSuppliers = new List<GpConnectInteractionForSupplier>
        {
            new GpConnectInteractionForSupplier() { Id = 1, Value = "Access Record: HTML", Selected = false },
            new GpConnectInteractionForSupplier() { Id = 2, Value = "Access Record: Structured", Selected = true },
            new GpConnectInteractionForSupplier() { Id = 3, Value = "Appointment Management", Selected = true },
            new GpConnectInteractionForSupplier() { Id = 4, Value = "Send Document", Selected = false }
        };

        var interactionsList = new List<GpConnectInteractions>() { GpConnectInteractions.AccessRecordStructured, GpConnectInteractions.AppointmentManagement };

        _mockTempDataProviderService.Setup(mtdps => mtdps.PutItem(TempDataConstants.SELECTEDGPCONNECTINTERACTIONFORSUPPLIER, interactionsList)).Verifiable();

        var softwareSupplierModel = new SystemSupplierModel(_mockOptions.Object, _mockSupplierService.Object, _mockTempDataProviderService.Object)
        {
            SelectedSoftwareSupplierId = "1",
            GpConnectInteractionForSupplierList = gpConnectInteractionForSuppliers
        };

        await softwareSupplierModel.OnPostNextAsync();

        _mockTempDataProviderService.Verify(mtdps => mtdps.PutItem(TempDataConstants.SELECTEDSOFTWARESUPPLIERID, "1"), Times.Once);
        _mockTempDataProviderService.Verify(mtdps => mtdps.PutItem(TempDataConstants.SELECTEDGPCONNECTINTERACTIONFORSUPPLIER, interactionsList), Times.Once);
    }
}
