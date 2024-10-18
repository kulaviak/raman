using FluentAssertions;
using NUnit.Framework;
using Raman.File;

namespace Raman.Test;

[TestFixture]
public class TxtLineParserTest
{
    [Test]
    public void TestTryParseLine_WhenNumbersSeparatedBySpace()
    {
        var points = new TxtLineParser().ParseLine("0.1 0.2");
        points[0].Should().Be(0.1);
        points[1].Should().Be(0.2);
    }
        
    [Test]
    public void TestTryParseLine_WhenNumbersSeparatedByTab()
    {
        var points = new TxtLineParser().ParseLine("0.1\t0.2");
        points[0].Should().Be(0.1);
        points[1].Should().Be(0.2);
    }
        
    [Test]
    public void TestTryParseLine_WhenNumbersSeparatedByTabAndSpace()
    {
        var points = new TxtLineParser().ParseLine("0.1\t 0.2");
        points[0].Should().Be(0.1);
        points[1].Should().Be(0.2);
    }
        
    [Test]
    public void TestTryParseLine_WhenDelimiterIsDot()
    {
        var points = new TxtLineParser().ParseLine("0.1\t0.2");
        points[0].Should().Be(0.1);
        points[1].Should().Be(0.2);
    }
        
    [Test]
    public void TestTryParseLine_WhenDelimiterIsComma()
    {
        var points = new TxtLineParser().ParseLine("0,1\t 0,2");
        points[0].Should().Be(0.1);
        points[1].Should().Be(0.2);
    }
}