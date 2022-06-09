using Microsoft.AspNetCore.Html;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers;

public static class BooleanExtensions
{
  public static HtmlString BooleanToHtml(this bool input) =>
      input switch
      {
        _ => input ? new HtmlString("&#x2713;") : new HtmlString("&#x2717;")
      };

  public static HtmlString StringToBooleanHtml(this string input) =>
      input switch
      {
        _ => bool.TryParse(input, out _) ? new HtmlString("&#x2713;") : new HtmlString("&#x2717;")
      };

  public static bool StringToBoolean(this string input)
  {
    bool flag;
    if (bool.TryParse(input, out flag))
      return flag;
    else
      return false;
  }

  public static string BooleanToYesNo(this bool input) =>
      input switch
      {
        _ => input ? "Yes" : "No"
      };

  public static string StringToYesNo(this string input)
  {
    if (input == bool.TrueString) return "Yes";
    if (input == bool.FalseString) return "No";
    return input;
  }
}
