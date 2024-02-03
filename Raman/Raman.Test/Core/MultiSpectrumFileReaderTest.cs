using FluentAssertions;
using NUnit.Framework;
using Raman.Core;

namespace Raman.Test;

[TestFixture]
public class MultiSpectrumFileReaderTest
{
    [Test]
    public void TestTryReadFile()
    {
        var spectraPoints = new MultiSpectrumFileReader("./Core/MultiSpectrumFileReaderTest_TestTryReadFile.txt").TryReadFile();
        spectraPoints.Should().HaveCount(2);

        var firstSpectrum = spectraPoints[0];
        firstSpectrum[0].Should().BeEquivalentTo(new Point(701.138m, 100));
        firstSpectrum[1].Should().BeEquivalentTo(new Point(702.299m, -85.4052m));
        firstSpectrum[2].Should().BeEquivalentTo(new Point(703.458m, -66.6577m));
        
        var secondSpectrum = spectraPoints[1];
        secondSpectrum[0].Should().BeEquivalentTo(new Point(701.138m, 100));
        secondSpectrum[1].Should().BeEquivalentTo(new Point(702.299m, -85.4052m));
        secondSpectrum[2].Should().BeEquivalentTo(new Point(703.458m, -53.8618m));
    }
}