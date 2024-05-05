using System;
using System.Drawing;
using NUnit.Framework;
using Raman.Core;

namespace Raman.Test;

[TestFixture]
public class ClosestSpectrumCalculatorTest
{
    [Test]
    public void TestGetDistanceToSegment()
    {
        var actual = ClosestSpectrumCalculator.GetDistanceToSegment(new Point(0, 0), new Point(1, 0), new Point(0, 1));
        Assert.AreEqual(Math.Sqrt(2)/2, actual, 0.001);
    }
}