using TideScraper.Core.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TideScraper.Core.Tests
{
    [TestClass]
    public class SignatureGeneratorFactoryTest
    {
        [TestMethod]
        public void GetGenerator_ReturnsNullIfNoGeneratorIsFound() 
        {
            var factory = new SignatureGeneratorFactory();
            var generator = factory.GetGenerator("nothing");
            Assert.IsNull(generator);
        }

        [TestMethod]
        public void GetGenerator_ReturnsTheSpecifiedGenerator()
        {
            var factory = new SignatureGeneratorFactory();
            var generator = factory.GetGenerator(SignatureGeneratorNames.HmacSha1);

            Assert.IsInstanceOfType(generator, typeof(HmacSha1SignatureGenerator));
        }
    }
}
