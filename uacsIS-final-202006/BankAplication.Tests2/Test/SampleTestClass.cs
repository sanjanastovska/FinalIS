using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankAplication.Tests2.Test
{
    [TestFixture]
    class SampleTestClass
    {
        [Test]
        public void DummyPassTest()
        {
            Assert.True(true);
        }

        [Test]
        public void DummyFailTest()
        {
            Assert.False(false);
        }
    }
}
