﻿using FluentAssertions;
using NUnit.Framework;
using Raman.Core;
using Raman.File;

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
        firstSpectrum[0].Should().BeEquivalentTo(new ValuePoint(701.138, 100));
        firstSpectrum[1].Should().BeEquivalentTo(new ValuePoint(702.299, -85.4052));
        firstSpectrum[2].Should().BeEquivalentTo(new ValuePoint(703.458, -66.6577));
        
        var secondSpectrum = spectraPoints[1];
        secondSpectrum[0].Should().BeEquivalentTo(new ValuePoint(701.138, 100));
        secondSpectrum[1].Should().BeEquivalentTo(new ValuePoint(702.299, -85.4052));
        secondSpectrum[2].Should().BeEquivalentTo(new ValuePoint(703.458, -53.8618));
    }
}