using FluentAssertions;
using NUnit.Framework;
using Raman.Core;
using Raman.File;

namespace Raman.Test;

[TestFixture]
public class SingleSpectrumFileReaderTest
{
    [Test]
    public void TestTryParseLine_WhenNumbersSeparatedBySpace()
    {
        var point = SingleSpectrumFileReader.TryParseLine("0.1 0.2");
        point.X.Should().Be(0.1);
        point.Y.Should().Be(0.2);
    }
        
    [Test]
    public void TestTryParseLine_WhenNumbersSeparatedByTab()
    {
        var point = SingleSpectrumFileReader.TryParseLine("0.1\t0.2");
        point.X.Should().Be(0.1);
        point.Y.Should().Be(0.2);
    }
        
    [Test]
    public void TestTryParseLine_WhenNumbersSeparatedByTabAndSpace()
    {
        var point = SingleSpectrumFileReader.TryParseLine("0.1\t 0.2");
        point.X.Should().Be(0.1);
        point.Y.Should().Be(0.2);
    }
        
    [Test]
    public void TestTryParseLine_WhenDelimiterIsDot()
    {
        var point = SingleSpectrumFileReader.TryParseLine("0.1\t0.2");
        point.X.Should().Be(0.1);
        point.Y.Should().Be(0.2);
    }
        
    [Test]
    public void TestTryParseLine_WhenDelimiterIsComma()
    {
        var point = SingleSpectrumFileReader.TryParseLine("0,1\t 0,2");
        point.X.Should().Be(0.1);
        point.Y.Should().Be(0.2);
    }
}