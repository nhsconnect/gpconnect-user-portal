using Dapper;
using GpConnect.NationalDataSharingPortal.Api.Dal.Interfaces;
using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using GpConnect.NationalDataSharingPortal.Api.Service;
using GpConnect.NationalDataSharingPortal.Api.Stores;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Service;

public class SupplierServiceTest
{
    private readonly Mock<ISupplierStore> _mockSupplierStore;
    private readonly SupplierService _sut;

    public SupplierServiceTest()
    {
        _mockSupplierStore = new Mock<ISupplierStore>();
        _sut = new SupplierService(_mockSupplierStore.Object);
    }

    [Fact]
    public void CallConstructor_WithExpectedParameters_ReturnsNotNull()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public async Task Get_Suppliers_ReturnsListOfSuppliers()
    {
        var dictionary = new Dictionary<int, Supplier>();
        _mockSupplierStore.Setup(d => d.GetSupplierData()).Returns(dictionary);

        var result = await _sut.GetSuppliers();
        Assert.IsAssignableFrom<IEnumerable<Supplier>>(result);
    }

    [Fact]
    public async Task Get_Supplier_ReturnsSupplier()
    {
        var dictionary = new Dictionary<int, Supplier>() {
            { 1, new Supplier() }
        };

        _mockSupplierStore.Setup(d => d.GetSupplierData()).Returns(dictionary);

        var id = 1;
        var result = await _sut.GetSupplier(id);
        Assert.IsAssignableFrom<Supplier>(result);
    }
}
