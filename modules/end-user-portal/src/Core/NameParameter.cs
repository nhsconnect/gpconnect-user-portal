namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core;

public class NameParameterAttribute : Attribute
{
    public string NameParameter { get; protected set; } = "";

    public NameParameterAttribute(string value)
    {
        NameParameter = value;
    }
}
