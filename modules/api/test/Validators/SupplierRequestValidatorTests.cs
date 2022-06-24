using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using GpConnect.NationalDataSharingPortal.Api.Validators;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Validators;

public class SupplierRequestValidatorTests
{
    private readonly Mock<ISupplierService> _mockService;
    private readonly SupplierRequestValidator _sut;

    public SupplierRequestValidatorTests()
    {
        _mockService = new Mock<ISupplierService>();
        _sut = new SupplierRequestValidator(_mockService.Object);
    }

    [Fact]
    public void CanConstruct()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void CannotConstructWithNullSupplierService()
    {
        Assert.Throws<ArgumentNullException>(() => new SupplierRequestValidator(default(ISupplierService)));
    }

    [Fact]
    public async Task CanCallIsValidUpdate()
    {
        var request = new SupplierUpdateRequest { SupplierId = 1, SupplierValue = "TestValue1" };
        var result = await _sut.IsValidUpdate(request);
        Assert.True(result.RequestValid);
    }

    [Fact]
    public async Task CannotCallIsValidUpdateWithNullRequest()
    {
        await Assert.ThrowsAsync<NullReferenceException>(() => _sut.IsValidUpdate(default(SupplierUpdateRequest)));
    }

    [Fact]
    public async Task CanCallIsValidDisable()
    {
        var request = new SupplierDisableRequest { SupplierId = 1, SupplierDisabled = true };
        var result = await _sut.IsValidDisable(request);
        Assert.True(result.RequestValid);
    }

    [Fact]
    public async Task CannotCallIsValidDisableWithNullRequest()
    {
        await Assert.ThrowsAsync<NullReferenceException>(() => _sut.IsValidDisable(default(SupplierDisableRequest)));
    }

    [Fact]
    public void CanCallIsValidAdd()
    {
        var request = new SupplierAddRequest { SupplierValue = "TestValue1" };
        var result = _sut.IsValidAdd(request);
        Assert.True(result);
    }

    [Fact]
    public void CannotCallIsValidAddWithNullRequest()
    {
        Assert.Throws<NullReferenceException>(() => _sut.IsValidAdd(default(SupplierAddRequest)));
    }
}
