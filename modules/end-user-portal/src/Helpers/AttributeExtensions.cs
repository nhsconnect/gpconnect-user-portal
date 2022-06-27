using GpConnect.NationalDataSharingPortal.EndUserPortal.Core;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers;

public static class AttributeExtensions
{
    public static string GetQueryStringParameter<T>(this T val) where T : Enum
    {
        return GetAttr<QueryStringParameterAttribute, T>(val)?.QueryStringParameter ?? "";
    }

    public static string GetDisplayParameter<T>(this T val) where T : Enum
    {
        return GetAttr<DisplayParameterAttribute, T>(val)?.DisplayParameter ?? "";
    }

    private static TAttr GetAttr<TAttr, T>(T val) where TAttr : Attribute
    {
        return (TAttr)typeof(T)
            .GetField(val.ToString())
            ?.GetCustomAttributes(typeof(TAttr), false)
            ?.FirstOrDefault();
    }
}
