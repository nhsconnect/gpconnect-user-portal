using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test.Core.Data;

public class TempDataProviderServiceTest
{
    private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
    private readonly Mock<ITempDataDictionaryFactory> _mockTempDataDictionaryFactory;
    private readonly Mock<ITempDataDictionary> _mockTempDataDictionary;
    private readonly TempDataProviderService _sut;

    public TempDataProviderServiceTest()
    {
        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>(MockBehavior.Strict);
        var httpContext = new DefaultHttpContext();
        _mockHttpContextAccessor.Setup(h => h.HttpContext).Returns(httpContext);

        _mockTempDataDictionary = new Mock<ITempDataDictionary>();
        
        _mockTempDataDictionary.Setup(m => m.Remove(It.IsAny<string>())).Returns(true);
        _mockTempDataDictionary.Setup(m => m.Add(It.IsAny<KeyValuePair<string, object?>>())).Verifiable();

        _mockTempDataDictionaryFactory = new Mock<ITempDataDictionaryFactory>(MockBehavior.Strict);
        _mockTempDataDictionaryFactory.Setup(f => f.GetTempData(It.IsAny<HttpContext>())).Returns(_mockTempDataDictionary.Object);

        _sut = new TempDataProviderService(_mockHttpContextAccessor.Object, _mockTempDataDictionaryFactory.Object);        
    }

    [Theory]
    [InlineData("ItemKey", "ItemValue")]
    [InlineData("ItemKey", 1)]
    public void PutItem_AddsValueToTempData_ReturnsExpectedValue(string key, object value)
    {
        var json = JsonConvert.SerializeObject(value);
        _mockTempDataDictionary.Setup(m => m.Peek(It.IsAny<string>())).Returns(json);
        _sut.PutItem(key, value);
        Assert.Equal(_sut.GetItem<string>(key), value.ToString());
    }

    [Theory]
    [InlineData("ItemKey", "ItemValue")]
    [InlineData("ItemKey", 1)]
    public void Remove_RemoveItemFromTempData_ReturnsExpectedValue(string key, object value)
    {
        var json = JsonConvert.SerializeObject(value);
        _mockTempDataDictionary.Setup(m => m.Peek(It.IsAny<string>())).Returns(json);
        _sut.PutItem(key, value);
        _sut.RemoveItem(key);
        Assert.False(_sut.HasItems);
    }
}
