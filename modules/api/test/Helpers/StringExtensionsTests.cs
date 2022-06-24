using GpConnect.NationalDataSharingPortal.Api.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Helpers;

public static class StringExtensionsTests
{
    [Fact]
    public static void CanCallFlatten()
    {
        var elems = new List<string>() { "Hello", "World" };
        var separator = ",";
        var result = StringExtensions.Flatten(elems, separator);
        Assert.Equal("Hello,World", result);
    }

    [Fact]
    public static void CannotCallFlattenWithNullElems()
    {
        Assert.Throws<ArgumentNullException>(() => StringExtensions.Flatten(default(IEnumerable), ","));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public static void CannotCallFlattenWithInvalidSeparator(string value)
    {
        Assert.Throws<ArgumentNullException>(() => StringExtensions.Flatten(default(IEnumerable), value));
    }

    [Fact]
    public static void CanCallFirstCharToUpper()
    {
        var input = "testValue1";
        var restToLower = false;
        var result = input.FirstCharToUpper(restToLower);
        Assert.Equal("TestValue1", result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public static void CannotCallFirstCharToUpperWithInvalidInput(string value)
    {
        Assert.Throws<ArgumentNullException>(() => value.FirstCharToUpper(true));
    }

    [Fact]
    public static void CanCallCoalesce()
    {
        var strings = new[] { "TestValue1", "TestValue2", "TestValue3" };
        var result = StringExtensions.Coalesce(strings);
        Assert.Equal("TestValue1", result);
    }

    [Fact]
    public static void CannotCallCoalesceWithNullStrings()
    {
        Assert.Throws<ArgumentNullException>(() => StringExtensions.Coalesce(default(string[])));
    }

    [Fact]
    public static void CanCallSearchAndReplace()
    {
        var input = "TestValue1";
        var replacementValues = new Dictionary<string, string>() { { "a", "b" } };
        var result = input.SearchAndReplace(replacementValues);
        Assert.Equal(result, "TestVblue1");
    }

    [Fact]
    public static void CannotCallSearchAndReplaceWithNullReplacementValues()
    {
        Assert.Throws<ArgumentNullException>(() => "TestValue1".SearchAndReplace(default(Dictionary<string, string>)));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public static void CannotCallSearchAndReplaceWithInvalidInput(string value)
    {
        Assert.Throws<ArgumentNullException>(() => value.SearchAndReplace(default(Dictionary<string, string>)));
    }

    [Fact]
    public static void CanCallFlattenStrings()
    {
        var strings = new[] { "TestValue1", "TestValue2", "TestValue3" };
        var result = StringExtensions.FlattenStrings(strings);
        Assert.Equal(result, "TestValue1, TestValue2, TestValue3");
    }

    [Fact]
    public static void CannotCallFlattenStringsWithNullStrings()
    {
        Assert.Throws<ArgumentNullException>(() => StringExtensions.FlattenStrings(default(string[])));
    }

    [Fact]
    public static void CanCallPluraliser()
    {
        var input = "Picture";
        var countValue = 8;
        var startTag = "";
        var endTag = "s";
        var result = input.Pluraliser(countValue, startTag, endTag);
        Assert.Equal(result, "Pictures");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public static void CannotCallPluraliserWithInvalidInput(string value)
    {
        Assert.Throws<ArgumentNullException>(() => value.Pluraliser(1, "TestValue1", "TestValue2"));
    }
}
