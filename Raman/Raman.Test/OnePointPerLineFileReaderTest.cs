using FluentAssertions;
using NUnit.Framework;
using Raman.Core;

namespace Raman.Test
{
    [TestFixture]
    public class OnePointPerLineFileReaderTest
    {
        [Test]
        public void TestTryParseLine_WhenNumbersSeparatedBySpace()
        {
            var point = OnePointPerLineFileReader.TryParseLine("0.1 0.2");
            point.X.Should().Be(0.1m);
            point.Y.Should().Be(0.2m);
        }
        
        [Test]
        public void TestTryParseLine_WhenNumbersSeparatedByTab()
        {
            var point = OnePointPerLineFileReader.TryParseLine("0.1\t0.2");
            point.X.Should().Be(0.1m);
            point.Y.Should().Be(0.2m);
        }
        
        [Test]
        public void TestTryParseLine_WhenNumbersSeparatedByTabAndSpace()
        {
            var point = OnePointPerLineFileReader.TryParseLine("0.1\t 0.2");
            point.X.Should().Be(0.1m);
            point.Y.Should().Be(0.2m);
        }
        
        [Test]
        public void TestTryParseLine_WhenDelimiterIsDot()
        {
            var point = OnePointPerLineFileReader.TryParseLine("0.1\t0.2");
            point.X.Should().Be(0.1m);
            point.Y.Should().Be(0.2m);
        }
        
        [Test]
        public void TestTryParseLine_WhenDelimiterIsComma()
        {
            var point = OnePointPerLineFileReader.TryParseLine("0,1\t 0,2");
            point.X.Should().Be(0.1m);
            point.Y.Should().Be(0.2m);
        }
    }
}