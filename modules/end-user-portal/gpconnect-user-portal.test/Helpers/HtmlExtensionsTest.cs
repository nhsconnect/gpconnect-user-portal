using System;
using System.Web;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test.Helpers;

public class HtmlExtensionsTest
{
    [Fact]
    public void AsMailHyperlink_ReturnsAsHyperLink()
    {
        var result = "memyselfI".AsMailHyperlink();

        Assert.Equal("<a", result?.Value?.Substring(0,2));
    }

    [Fact]
    public void AsMailHyperlink_PrefixesHrefWithMail()
    {
        var result = "memyselfI".AsMailHyperlink();
        var index = result?.Value?.IndexOf("href=\"");
        var prefix = result?.Value?.Substring(index.Value + 6, 7);

        Assert.Equal("mailto:", prefix);
    }

    [Fact]
    public void AsMailHyperlink_EncodesHTMLCharacters_ObsufcatesInput()
    {
        var input = "memyselfI";
        var expectedOutputString = "&#109;&#101;&#109;&#121;&#115;&#101;&#108;&#102;&#73;";
        
        var result = input.AsMailHyperlink();
        var raw = result.ToString();

        var closingBracket = raw.IndexOf(">");
        var closeTag = raw.IndexOf("</a>");

        Assert.DoesNotContain(input, result.ToString());
        Assert.Contains(expectedOutputString, result.ToString());
        Assert.Equal(input, HttpUtility.HtmlDecode(raw.Substring(closingBracket + 1, closeTag - closingBracket - 1)));
    }

    [Fact]
    public void AsPhoneHyperlink_ReturnsAsHyperLink()
    {
        var result = "012456".AsPhoneHyperlink();

        Assert.Equal("<a", result?.Value?.Substring(0,2));
    }

    [Fact]
    public void AsPhoneHyperlink_PrefixesHrefWithTel()
    {
        var result = "123456".AsPhoneHyperlink();
        var index = result?.Value?.IndexOf("href=\"");
        var prefix = result?.Value?.Substring(index.Value + 6, 4);

        Assert.Equal("tel:", prefix);
    }

    [Fact]
    public void AsPhoneHyperlink_EncodesHTMLCharacters_ObsufcatesInput()
    {
        var input = "123456";
        var expectedOutputString = "&#49;&#50;&#51;&#52;&#53;&#54;";
        
        var result = input.AsPhoneHyperlink();
        var raw = result.ToString();

        var closingBracket = raw.IndexOf(">");
        var closeTag = raw.IndexOf("</a>");

        Assert.DoesNotContain(input, result.ToString());
        Assert.Contains(expectedOutputString, result.ToString());
        Assert.Equal(input, HttpUtility.HtmlDecode(raw.Substring(closingBracket + 1, closeTag - closingBracket - 1)));
    }
}