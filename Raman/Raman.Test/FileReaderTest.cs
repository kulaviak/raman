using System;
using FluentAssertions;
using NUnit.Framework;
using Raman.Core;

namespace Raman.Test
{
    [TestFixture]
    public class FileReaderTest
    {
        [Test]
        public void TestTryParseLine1()
        {
            var point = FileReader.TryParseLine("0.1 0.2");
            point.X.Should().Be(0.1m);
            point.Y.Should().Be(0.2m);
        }
        
        [Test]
        public void TestTryParseLine2()
        {
            var point = FileReader.TryParseLine("0.1\t 0.2");
            point.X.Should().Be(0.1m);
            point.Y.Should().Be(0.2m);
        }
    }
}