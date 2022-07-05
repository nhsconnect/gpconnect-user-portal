namespace GpConnect.NationalDataSharingPortal.Api.Dal.Extensions;

public static class DbBooleanExtensions
{
    public static string ToDbString(this bool input) => input ? "True" : "False";
    
}