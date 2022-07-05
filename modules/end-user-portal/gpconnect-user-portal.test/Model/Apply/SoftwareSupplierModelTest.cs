using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Data.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test.Pages.Apply;

public class SoftwareSupplierModelTest
{
    private readonly Mock<ISupplierService> _mockSupplierService;
    private readonly Mock<ITempDataProviderService> _mockTempDataProviderService;
    private readonly Mock<IOptions<ApplicationParameters>> _mockOptions;

    public SoftwareSupplierModelTest()
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

        var softwareSupplierModel = new SoftwareSupplierModel(_mockOptions.Object, _mockSupplierService.Object, _mockTempDataProviderService.Object)
        {
            SelectedSoftwareSupplierNameId = 1,
            DisplaySoftwareSupplierProducts = true
        };

        Assert.Equal(1, softwareSupplierModel.SelectedSoftwareSupplierNameId);
        Assert.Equal(2, softwareSupplierModel.SoftwareSupplierResultList[1].SoftwareSupplierId);
        Assert.Equal("Software Supplier 2", softwareSupplierModel.SoftwareSupplierResultList[1].SoftwareSupplierName);
        Assert.Equal(1, softwareSupplierModel.SelectedSoftwareSupplier.SoftwareSupplierId);
        Assert.Equal("Software Supplier 1", softwareSupplierModel.SelectedSoftwareSupplier.SoftwareSupplierName);
        Assert.True(softwareSupplierModel.IsSelectedSoftwareSupplier);
        Assert.Equal(3, softwareSupplierModel.SoftwareSupplierResultList.Count);
    }

    [Fact]
    public void OnGet_GetSoftwareSupplierProductsList_ReturnsListOfSoftwareSupplierProductsFromTempData()
    {
        var softwareSupplierProductList = new List<SoftwareSupplierProductResult>
        {
            new SoftwareSupplierProductResult() { SoftwareSupplierProductId = 1, SoftwareSupplierProduct = "Access Record: HTML", Selected = false },
            new SoftwareSupplierProductResult() { SoftwareSupplierProductId = 2, SoftwareSupplierProduct = "Access Record: Structured", Selected = true },
            new SoftwareSupplierProductResult() { SoftwareSupplierProductId = 3, SoftwareSupplierProduct = "Appointment Management", Selected = true },
            new SoftwareSupplierProductResult() { SoftwareSupplierProductId = 4, SoftwareSupplierProduct = "Send Document", Selected = false }
        };

        _mockTempDataProviderService.Setup(mtd => mtd.PutItem(It.IsAny<string>(), softwareSupplierProductList));
        _mockTempDataProviderService.Setup(mtd => mtd.GetItem<List<SoftwareSupplierProductResult>>(It.IsAny<string>())).Returns(softwareSupplierProductList);
        
        var softwareSupplierModel = new SoftwareSupplierModel(_mockOptions.Object, _mockSupplierService.Object, _mockTempDataProviderService.Object)
        {
            SelectedSoftwareSupplierNameId = 1,
            DisplaySoftwareSupplierProducts = true,
            SoftwareSupplierProductList = softwareSupplierProductList
        };

        Assert.Equal(4, softwareSupplierModel.SoftwareSupplierProductList.Count);
        Assert.Equal(1, softwareSupplierModel.SoftwareSupplierProductList[0].SoftwareSupplierProductId);
        Assert.Equal("Access Record: Structured", softwareSupplierModel.SoftwareSupplierProductList[1].SoftwareSupplierProduct);
        Assert.True(softwareSupplierModel.SoftwareSupplierProductList[1].Selected);
    }

    [Fact]
    public void OnPost_IfValidEntry_RedirectsToNextPage()
    {
        var softwareSupplierProductList = new List<SoftwareSupplierProductResult>
        {
            new SoftwareSupplierProductResult() { SoftwareSupplierProductId = 1, SoftwareSupplierProduct = "Access Record: HTML", Selected = false },
            new SoftwareSupplierProductResult() { SoftwareSupplierProductId = 2, SoftwareSupplierProduct = "Access Record: Structured", Selected = true },
            new SoftwareSupplierProductResult() { SoftwareSupplierProductId = 3, SoftwareSupplierProduct = "Appointment Management", Selected = true },
            new SoftwareSupplierProductResult() { SoftwareSupplierProductId = 4, SoftwareSupplierProduct = "Send Document", Selected = false }
        };

        var softwareSupplierModel = new SoftwareSupplierModel(_mockOptions.Object, _mockSupplierService.Object, _mockTempDataProviderService.Object)
        {
            SelectedSoftwareSupplierNameId = 1,
            DisplaySoftwareSupplierProducts = true,
            SoftwareSupplierProductList = softwareSupplierProductList,
            HasSelectedSoftwareSupplierProducts = true
        };

        var result = softwareSupplierModel.OnPostNextAsync();
        Assert.IsType<RedirectToPageResult>(result);
    }

    [Fact]
    public void OnPostCheckSupplierProducts_IfTempDataHasSoftwareSupplierEntries_DisplaysSoftwareSupplierProducts()
    {
        _mockTempDataProviderService.Setup(mtd => mtd.HasItems).Returns(true);

        var softwareSupplierModel = new SoftwareSupplierModel(_mockOptions.Object, _mockSupplierService.Object, _mockTempDataProviderService.Object)
        {
            SelectedSoftwareSupplierNameId = 1,
            DisplaySoftwareSupplierProducts = true
        };

        var result = softwareSupplierModel.OnPostCheckSupplierProductsAsync();
        Assert.IsType<PageResult>(result);
    }

    [Fact]
    public void OnPostCheckSupplierProducts_IfTempDataHasNoSoftwareSupplierEntries_RedirectsToTimeoutPage()
    {
        _mockTempDataProviderService.Setup(mtd => mtd.HasItems).Returns(false);
        var softwareSupplierModel = new SoftwareSupplierModel(_mockOptions.Object, _mockSupplierService.Object, _mockTempDataProviderService.Object);

        var result = softwareSupplierModel.OnPostCheckSupplierProductsAsync();
        Assert.IsType<PageResult>(result);
    }

    [Fact]
    public void OnPost_IfTempDataHasNoData_RedirectsToTimeoutPage()
    {
        _mockTempDataProviderService.Setup(mtd => mtd.HasItems).Returns(false);
        var softwareSupplierProductList = new List<SoftwareSupplierProductResult>
        {
            new SoftwareSupplierProductResult() { SoftwareSupplierProductId = 1, SoftwareSupplierProduct = "Access Record: HTML", Selected = false },
            new SoftwareSupplierProductResult() { SoftwareSupplierProductId = 2, SoftwareSupplierProduct = "Access Record: Structured", Selected = true },
            new SoftwareSupplierProductResult() { SoftwareSupplierProductId = 3, SoftwareSupplierProduct = "Appointment Management", Selected = true },
            new SoftwareSupplierProductResult() { SoftwareSupplierProductId = 4, SoftwareSupplierProduct = "Send Document", Selected = false }
        };

        var softwareSupplierModel = new SoftwareSupplierModel(_mockOptions.Object, _mockSupplierService.Object, _mockTempDataProviderService.Object)
        {
            SelectedSoftwareSupplierNameId = 1,
            DisplaySoftwareSupplierProducts = true,
            SoftwareSupplierProductList = softwareSupplierProductList,
            HasSelectedSoftwareSupplierProducts = true
        };

        var result = softwareSupplierModel.OnPostNextAsync();
        Assert.IsType<RedirectToPageResult>(result);
    }

    [Fact]
    public void OnPostCheckSupplierProducts_IfInvalidModel_ReturnValidationError()
    {
        var softwareSupplierModel = new SoftwareSupplierModel(_mockOptions.Object, _mockSupplierService.Object, _mockTempDataProviderService.Object);
        softwareSupplierModel.ModelState.AddModelError("SelectedSoftwareSupplier", "You must select a value for Software Supplier Name");

        var result = softwareSupplierModel.OnPostCheckSupplierProductsAsync();
        Assert.IsType<PageResult>(result);
        Assert.True(softwareSupplierModel.ModelState.ErrorCount > 0);
    }
}
