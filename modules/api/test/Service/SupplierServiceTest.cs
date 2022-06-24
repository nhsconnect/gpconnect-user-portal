using Dapper;
using GpConnect.NationalDataSharingPortal.Api.Dal.Interfaces;
using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using GpConnect.NationalDataSharingPortal.Api.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Service;

public class SupplierServiceTest
{
    private readonly Mock<IDataService> _mockDataService;
    private readonly SupplierService _sut;

    public SupplierServiceTest()
    {
        _mockDataService = new Mock<IDataService>();
        _sut = new SupplierService(_mockDataService.Object);
    }

    [Fact]
    public void CanConstruct()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void CannotConstructWithNullDataService()
    {
        Assert.Throws<ArgumentNullException>(() => new SupplierService(default(IDataService)));
    }

    [Fact]
    public async Task CanCallGetSuppliers()
    {
        var list = Task.FromResult(new List<Supplier>());
        _mockDataService.Setup(d => d.ExecuteQuery<Supplier>(It.IsAny<string>(), It.IsAny<DynamicParameters>())).Returns(list);

        var result = await _sut.GetSuppliers();
        Assert.IsAssignableFrom<IEnumerable<Supplier>>(result);
    }

    [Fact]
    public async Task CanCallGetSupplier()
    {
        var single = Task.FromResult(new Supplier());
        _mockDataService.Setup(d => d.ExecuteQueryFirstOrDefault<Supplier>(It.IsAny<string>(), It.IsAny<DynamicParameters>())).Returns(single);

        var id = 1;
        var result = await _sut.GetSupplier(id);
        Assert.IsAssignableFrom<Supplier>(result);
    }

    [Fact]
    public async Task CanCallAddSupplier()
    {
        var single = Task.FromResult(new Supplier());
        _mockDataService.Setup(d => d.ExecuteQueryFirstOrDefault<Supplier>(It.IsAny<string>(), It.IsAny<DynamicParameters>())).Returns(single);

        var supplierAddRequest = new SupplierAddRequest { SupplierValue = "TestValue1" };
        var result = await _sut.AddSupplier(supplierAddRequest);
        Assert.IsAssignableFrom<Supplier>(result);
    }

    [Fact]
    public async Task CannotCallAddSupplierWithNullSupplierAddRequest()
    {
        await Assert.ThrowsAsync<NullReferenceException>(() => _sut.AddSupplier(default(SupplierAddRequest)));
    }

    [Fact]
    public async Task CannotCallDisableProductWithNullProductDisableRequest()
    {
        await Assert.ThrowsAsync<NullReferenceException>(() => _sut.DisableSupplier(default(SupplierDisableRequest)));
    }

    [Fact]
    public async Task CannotCallUpdateProductWithNullProductUpdateRequest()
    {
        await Assert.ThrowsAsync<NullReferenceException>(() => _sut.UpdateSupplier(default(SupplierUpdateRequest)));
    }
}
