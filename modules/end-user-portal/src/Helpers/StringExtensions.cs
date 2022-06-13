using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;
using Microsoft.AspNetCore.Html;
using System.Text;
using System.Web;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers;

public static class StringExtensions
{
    public static HtmlString CreateHtmlString(this string input, LinkTypeEnums linkTypeEnums)
    {
        var encodedString = HtmlEncode(input);
        switch(linkTypeEnums)
        {
            case LinkTypeEnums.MailTo:
                return new HtmlString($"<a href=\"mailto:{encodedString}\">{encodedString}</a>");
            case LinkTypeEnums.Telephone:
                return new HtmlString($"<a href=\"tel:{encodedString}\">{encodedString}</a>");
            default:
                return new HtmlString(string.Empty);
        }        
    }

    private static string HtmlEncode(string input)
    {
        var chars = HttpUtility.HtmlEncode(input).ToCharArray();
        var result = new StringBuilder(input.Length + (int)(input.Length * 0.1));

        foreach (char c in chars)
        {
            int value = Convert.ToInt32(c);
            result.AppendFormat("&#{0};", value);
        }

        return result.ToString();
    }
}
