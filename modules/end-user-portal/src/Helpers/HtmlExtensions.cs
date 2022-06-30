using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;
using Microsoft.AspNetCore.Html;
using System.Text;
using System.Web;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers;

public static class HtmlExtensions
{
    public static HtmlString AsMailHyperlink(this string input)
    {
        var encodedString = ObsufcateInput(input);
        return new HtmlString($"<a href=\"mailto:{encodedString}\">{encodedString}</a>");        
    }

    public static HtmlString AsPhoneHyperlink(this string input)
    {
        var encodedString = ObsufcateInput(input);
        return new HtmlString($"<a href=\"tel:{encodedString}\">{encodedString}</a>");        
    }

    private static string ObsufcateInput(string input)
    {
        // encode our input to capture any non-ascii characters
        var chars = HttpUtility.HtmlEncode(input).ToCharArray();

        var builder = new StringBuilder();

        // encode the raw input to prevent bots scrapping the data.
        foreach (char c in chars)
        {
            int value = Convert.ToInt32(c);
            builder.AppendFormat("&#{0};", value);
        }

        return builder.ToString();
    }
}
