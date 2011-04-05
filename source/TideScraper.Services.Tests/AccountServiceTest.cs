using TideScraper.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using TideScraper.Data;
using TideScraper.Core.Models;

namespace TideScraper.Services.Tests
{
    [TestClass]
    public class CreateApplication_AccountServiceTest
    {
        private Mock<IAccountRepository> _accountRepositoryMock;

        [TestInitialize]
        public void TestInitialize()
        {
            _accountRepositoryMock = new Mock<IAccountRepository>();
        }

        [TestMethod]
        public void FailsWhenDetailIsInvalid()
        {
            var svc = new AccountService(_accountRepositoryMock.Object);
            var result = svc.CreateApplication(new CreateApplicationDetail());

            Assert.IsFalse(result.Success);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage));
        }

        [TestMethod]
        public void FailsWhenDetailIsNull()
        {
            var svc = new AccountService(_accountRepositoryMock.Object);
            var result = svc.CreateApplication(null);

            Assert.IsFalse(result.Success);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage));
        }

        [TestMethod]
        public void CreatesAccountWhenDataIsValid()
        {
            var svc = new AccountService(_accountRepositoryMock.Object);
            var result = svc.CreateApplication(GetValidData());

            Assert.IsTrue(result.Success);
            dynamic newAccount = result.Result;

            Assert.IsFalse(string.IsNullOrWhiteSpace(newAccount.PublicKey));
            Assert.IsFalse(string.IsNullOrWhiteSpace(newAccount.PrivateKey));
            Assert.IsFalse(string.IsNullOrWhiteSpace(newAccount.ApplicationId));
        }

        [TestMethod]
        public void ApplicationIsSavedToDBWhenDataIsValid()
        {
            var svc = new AccountService(_accountRepositoryMock.Object);
            var result = svc.CreateApplication(GetValidData());

            Assert.IsTrue(result.Success);
            _accountRepositoryMock.Verify(x => x.CreateApplication(It.IsAny<Application>()), Times.Once(), "Account not created");
        }

        [TestMethod]
        public void FailsWhenAccountDoesntExist()
        {
            _accountRepositoryMock.Setup(x => x.GetAccountById(It.IsAny<string>())).Returns<Account>(null);

            var svc = new AccountService(_accountRepositoryMock.Object);
            var result = svc.CreateApplication(GetValidData());

            Assert.IsFalse(result.Success);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage));
        }

        private CreateApplicationDetail GetValidData()
        {
            return new CreateApplicationDetail
            {
                ApplicationName = "appname",
                ApplicationUrl = "appurl",
                EmailAddress = "email@email.com",
                FirstName = "first",
                LastName = "last",
                AccountId = "accountid"
            };
        }
    }
}
