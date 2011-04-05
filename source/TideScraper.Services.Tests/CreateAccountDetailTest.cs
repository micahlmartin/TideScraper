using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TideScraper.Services.Tests
{
    [TestClass]
    public class CreateAccountDetailTest
    {
        [TestMethod]
        public void Validate_FalseWhenEmailAddressIsMissing()
        {
            var detail = new CreateApplicationDetail
            {
                ApplicationName = "appname",
                ApplicationUrl = "appurl",
                EmailAddress = "",
                FirstName = "first",
                LastName = "last",
                AccountId = "accountid"
            };

            Assert.IsFalse(detail.Validate());
            Assert.AreEqual(1, detail.Errors.Count());
            Assert.AreEqual("EmailAddress", detail.Errors.ElementAt(0).PropertyName);

            detail.EmailAddress = null;

            Assert.IsFalse(detail.Validate());
            Assert.AreEqual(1, detail.Errors.Count());
            Assert.AreEqual("EmailAddress", detail.Errors.ElementAt(0).PropertyName);
        }

        [TestMethod]
        public void Validate_FalseWhenEmailAddressIsInvalid()
        {
            var detail = new CreateApplicationDetail
            {
                ApplicationName = "appname",
                ApplicationUrl = "appurl",
                EmailAddress = "email",
                FirstName = "first",
                LastName = "last",
                AccountId = "accountid"
            };

            Assert.IsFalse(detail.Validate());
            Assert.AreEqual(1, detail.Errors.Count());
            Assert.AreEqual("EmailAddress", detail.Errors.ElementAt(0).PropertyName);
        }

        [TestMethod]
        public void Validate_FalseWhenFirstNameIsMissing()
        {
            var detail = new CreateApplicationDetail
            {
                ApplicationName = "appname",
                ApplicationUrl = "appurl",
                EmailAddress = "email@email.com",
                FirstName = "",
                LastName = "last",
                AccountId = "accountid"
            };

            Assert.IsFalse(detail.Validate());
            Assert.AreEqual(1, detail.Errors.Count());
            Assert.AreEqual("FirstName", detail.Errors.ElementAt(0).PropertyName);

            detail.FirstName = null;

            Assert.IsFalse(detail.Validate());
            Assert.AreEqual(1, detail.Errors.Count());
            Assert.AreEqual("FirstName", detail.Errors.ElementAt(0).PropertyName);
        }

        [TestMethod]
        public void Validate_FalseWhenLastNameIsMissing()
        {
            var detail = new CreateApplicationDetail
            {
                ApplicationName = "appname",
                ApplicationUrl = "appurl",
                EmailAddress = "email@email.com",
                FirstName = "first",
                LastName = "",
                AccountId = "accountid"
            };

            Assert.IsFalse(detail.Validate());
            Assert.AreEqual(1, detail.Errors.Count());
            Assert.AreEqual("LastName", detail.Errors.ElementAt(0).PropertyName);
            detail.LastName = null;

            Assert.IsFalse(detail.Validate());
            Assert.AreEqual(1, detail.Errors.Count());
            Assert.AreEqual("LastName", detail.Errors.ElementAt(0).PropertyName);
        }

        [TestMethod]
        public void Validate_FalseWhenApplicationNameIsMissing()
        {
            var detail = new CreateApplicationDetail
            {
                ApplicationName = "",
                ApplicationUrl = "appurl",
                EmailAddress = "email@email.com",
                FirstName = "first",
                LastName = "last",
                AccountId = "accountid"
            };

            Assert.IsFalse(detail.Validate());
            Assert.AreEqual(1, detail.Errors.Count());
            Assert.AreEqual("ApplicationName", detail.Errors.ElementAt(0).PropertyName);
            
            detail.ApplicationName = null;

            Assert.IsFalse(detail.Validate());
            Assert.AreEqual(1, detail.Errors.Count());
            Assert.AreEqual("ApplicationName", detail.Errors.ElementAt(0).PropertyName);
        }

        [TestMethod]
        public void Validate_FalseWhenApplicationUrlIsMissing()
        {
            var detail = new CreateApplicationDetail
            {
                ApplicationName = "appname",
                ApplicationUrl = "",
                EmailAddress = "email@email.com",
                FirstName = "first",
                LastName = "last",
                AccountId = "accountid"
            };

            Assert.IsFalse(detail.Validate());
            Assert.AreEqual(1, detail.Errors.Count());
            Assert.AreEqual("ApplicationUrl", detail.Errors.ElementAt(0).PropertyName);

            detail.ApplicationUrl = null;

            Assert.IsFalse(detail.Validate());
            Assert.AreEqual(1, detail.Errors.Count());
            Assert.AreEqual("ApplicationUrl", detail.Errors.ElementAt(0).PropertyName);
        }

        [TestMethod]
        public void Validate_FalseWhenAccountIdIsMissing()
        {
            var detail = new CreateApplicationDetail
            {
                ApplicationName = "appname",
                ApplicationUrl = "appurl",
                EmailAddress = "email@email.com",
                FirstName = "first",
                LastName = "last",
                AccountId = ""
            };

            Assert.IsFalse(detail.Validate());
            Assert.AreEqual(1, detail.Errors.Count());
            Assert.AreEqual("AccountId", detail.Errors.ElementAt(0).PropertyName);

            detail.AccountId = null;

            Assert.IsFalse(detail.Validate());
            Assert.AreEqual(1, detail.Errors.Count());
            Assert.AreEqual("AccountId", detail.Errors.ElementAt(0).PropertyName);
        }

        [TestMethod]
        public void Validate_TrueWhenAllDataIsValid()
        {
            var detail = new CreateApplicationDetail
            {
                ApplicationName = "appname",
                ApplicationUrl = "appurl",
                EmailAddress = "email@email.com",
                FirstName = "first",
                LastName = "last",
                AccountId = "accountid"
            };

            Assert.IsTrue(detail.Validate());
            Assert.IsFalse(detail.Errors.Any());
        }
    }
}
