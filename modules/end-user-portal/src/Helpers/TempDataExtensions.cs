using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers;

public static class TempDataExtensions
{
    public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
    {
        if (value != null)
        {
            tempData[key] = JsonConvert.SerializeObject(value);
            tempData.Keep(key);
        }
    }

    public static void Remove(this ITempDataDictionary tempData, string key)
    {
        tempData.Remove(key);        
    }

    public static T? Get<T>(this ITempDataDictionary tempData, string key) where T : class
    {
        object? tempDataObject = tempData.Peek(key);
        return tempDataObject == null ? null : JsonConvert.DeserializeObject<T>((string)tempDataObject);
    }
}
