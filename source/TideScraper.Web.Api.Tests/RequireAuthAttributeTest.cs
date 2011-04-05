using TideScraper.Web.Api.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMoq;
using System.Collections.Specialized;

namespace TideScraper.Web.Api.Tests
{
    [TestClass]
    public class RequireAuthAttributeTest : RequireAuthAttribute
    {
        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestMethod]
        public void TEst()
        {
            //this.AuthorizeCore()
        }
    }
    
}