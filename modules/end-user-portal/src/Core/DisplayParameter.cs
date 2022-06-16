namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core;

public class DisplayParameterAttribute : Attribute
{
    public string DisplayParameter { get; protected set; } = "";

    public DisplayParameterAttribute(string value)
    {
        DisplayParameter = value;
    }
}
