namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Constants;

public class ValidationConstants
{
  public const string ALPHANUMERICCHARACTERSONLY = @"^[a-zA-Z0-9, ]*$";
  public const string UPPERCASELETTERSANDNUMBERSONLY = @"^[A-Z0-9]*$";
  public const string UPPERCASELETTERSANDNUMBERSANDSPACESONLY = @"^[A-Z0-9 ]*$";
  public const string ALPHANUMERICCHARACTERSWITHLEADINGTRAILINGSPACESANDCOMMASPACEONLY = @"^\s*[a-zA-Z0-9, ]*\s*$";
}
