using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Data.Interfaces;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Data;

public class TempDataProviderService : ITempDataProviderService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;
    private readonly ITempDataDictionary _tempDataDictionary;

    public TempDataProviderService(IHttpContextAccessor httpContextAccessor, ITempDataDictionaryFactory tempDataDictionaryFactory)
    {
        _httpContextAccessor = httpContextAccessor;
        _tempDataDictionaryFactory = tempDataDictionaryFactory;
        _tempDataDictionary = _tempDataDictionaryFactory.GetTempData(_httpContextAccessor.HttpContext);
    }

    public T? GetItem<T>(string key) where T : class
    {
        object? tempDataObject = _tempDataDictionary.Peek(key);
        return tempDataObject == null ? null : JsonConvert.DeserializeObject<T>((string)tempDataObject);
    }

    public bool HasItems => _tempDataDictionary != null && _tempDataDictionary.Count > 0;

    public void RemoveItem(string key)
    {
        _tempDataDictionary.Remove(key);
    }

    public void PutItem<T>(string key, T value) where T : class
    {
        _tempDataDictionary[key] = JsonConvert.SerializeObject(value);
        _tempDataDictionary.Keep(key);
    }

    public void RemoveAll()
    {
        _tempDataDictionary.Clear();
    }
}
