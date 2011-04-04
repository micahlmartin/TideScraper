using TideScraper.Core.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TideScraper.Core.Tests
{
    
    [TestClass]
    public class HmacSha1SignatureGeneratorTest
    {
        [TestMethod]
        public void Generate_IdenticalDataCreatesAnIdenticalHash()
        {
            var hasher = new HmacSha1SignatureGenerator();
            var hash1 = hasher.Generate("Secret key", "some data to hash");
            var hash2 = hasher.Generate("Secret key", "some data to hash");

            Assert.AreEqual(hash1, hash2);
        }
    }
}
