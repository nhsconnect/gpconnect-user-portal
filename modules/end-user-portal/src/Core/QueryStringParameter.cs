namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core;

public class QueryStringParameterAttribute : Attribute
{
    public string QueryStringParameter { get; protected set; } = "";

    public QueryStringParameterAttribute(string value)
    {
        QueryStringParameter = value;
    }
}
