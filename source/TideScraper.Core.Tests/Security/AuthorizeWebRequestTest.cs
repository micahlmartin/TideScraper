using TideScraper.Web.Api.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Web;
using Moq;
using TideScraper.Core.Security;
using System.Collections.Specialized;

namespace TideScraper.Web.Api.Tests
{
    [TestClass]
    public class AuthorizeWebRequestTest
    {
        private Mock<HttpRequestBase> _requestMock;
        private Mock<IConsumerRepository> _consumerRepositoryMock;
        private Mock<ISignatureGeneratorFactory> _signatureGeneratorFactoryMock;
        private Mock<IConsumer> _consumerMock;

        [TestInitialize]
        public void InitializeTest()
        {
            _requestMock = new Mock<HttpRequestBase>();
            _consumerRepositoryMock = new Mock<IConsumerRepository>();
            _signatureGeneratorFactoryMock = new Mock<ISignatureGeneratorFactory>();
            _consumerMock = new Mock<IConsumer>();
        }

        [TestMethod]
        public void Authorize_FailsWhenRequestContainsInvalidSignature()
        {
            var publicKey = "publickey";
            var timestamp = DateTimeOffset.UtcNow.Ticks.ToString();
            var signature = "invalid_signature";
            var signatureMethod = SignatureGeneratorNames.HmacSha1;

            _consumerMock.SetupGet(x => x.PrivateKey).Returns("privatekey");

            _consumerRepositoryMock.Setup(x => x.GetConsumerByPublicKey(It.IsAny<string>())).Returns(_consumerMock.Object);
            _signatureGeneratorFactoryMock.Setup(x => x.GetGenerator(It.IsAny<string>())).Returns(new HmacSha1SignatureGenerator());
            _requestMock.SetupGet(x => x.Form).Returns(new System.Collections.Specialized.NameValueCollection { { AuthorizationTokens.PublicKey, publicKey }, { AuthorizationTokens.Signature, signature }, { AuthorizationTokens.SignatureMethod, signatureMethod }, { AuthorizationTokens.TimeStamp, timestamp } });

            var requestAuthorizer = new AuthorizeWebRequest(_signatureGeneratorFactoryMock.Object, _consumerRepositoryMock.Object);
            var authResult = requestAuthorizer.Authorize(_requestMock.Object);

            Assert.AreEqual(authResult, AuthorizationResult.BadSignature);
        }

        [TestMethod]
        public void Authorize_FailsWhenConsumerDoesntExist()
        {
            var publicKey = "publickey";
            var timestamp = DateTimeOffset.UtcNow.Ticks.ToString();
            var signature = "signature";
            var signatureMethod = SignatureGeneratorNames.HmacSha1;


            _consumerRepositoryMock.Setup(x => x.GetConsumerByPublicKey(It.IsAny<string>())).Returns<IConsumer>(null);
            _signatureGeneratorFactoryMock.Setup(x => x.GetGenerator(It.IsAny<string>())).Returns(new HmacSha1SignatureGenerator());
            _requestMock.SetupGet(x => x.Form).Returns(new System.Collections.Specialized.NameValueCollection { { AuthorizationTokens.PublicKey, publicKey }, { AuthorizationTokens.Signature, signature }, { AuthorizationTokens.SignatureMethod, signatureMethod }, { AuthorizationTokens.TimeStamp, timestamp } });

            var requestAuthorizer = new AuthorizeWebRequest(_signatureGeneratorFactoryMock.Object, _consumerRepositoryMock.Object);
            var authResult = requestAuthorizer.Authorize(_requestMock.Object);

            Assert.AreEqual(authResult, AuthorizationResult.NonExistantConsumer);
        }

        [TestMethod]
        public void Authorize_FailsWhenDataIsIncomplete()
        {
            _consumerMock.SetupGet(x => x.PrivateKey).Returns("privatekey");

            _consumerRepositoryMock.Setup(x => x.GetConsumerByPublicKey(It.IsAny<string>())).Returns(_consumerMock.Object);
            _signatureGeneratorFactoryMock.Setup(x => x.GetGenerator(It.IsAny<string>())).Returns(new HmacSha1SignatureGenerator());
            _requestMock.SetupGet(x => x.Form).Returns(new System.Collections.Specialized.NameValueCollection());

            var requestAuthorizer = new AuthorizeWebRequest(_signatureGeneratorFactoryMock.Object, _consumerRepositoryMock.Object);
            var authResult = requestAuthorizer.Authorize(_requestMock.Object);

            Assert.AreEqual(authResult, AuthorizationResult.MissingData);
        }

        [TestMethod]
        public void Authorize_FailsWhenTimestampIsInThefutre()
        {
            _signatureGeneratorFactoryMock.Setup(x => x.GetGenerator(It.IsAny<string>())).Returns(new HmacSha1SignatureGenerator());

            var publicKey = "publicKey";
            var timestamp = DateTimeOffset.UtcNow.AddHours(1).Ticks.ToString();
            var signatureMethod = SignatureGeneratorNames.HmacSha1;
            var privateKey = "privateKey";
            var signature = _signatureGeneratorFactoryMock.Object.GetGenerator(signatureMethod).Generate(privateKey, string.Join("&", publicKey, signatureMethod, timestamp));

            _consumerMock.SetupGet(x => x.PrivateKey).Returns(privateKey);

            _consumerRepositoryMock.Setup(x => x.GetConsumerByPublicKey(It.IsAny<string>())).Returns(_consumerMock.Object);

            var nvc = new NameValueCollection{ 
                { AuthorizationTokens.PublicKey, publicKey }, 
                { AuthorizationTokens.Signature, signature }, 
                { AuthorizationTokens.SignatureMethod, signatureMethod }, 
                { AuthorizationTokens.TimeStamp, timestamp } };

            _requestMock.SetupGet(x => x.Form).Returns(nvc);

            var requestAuthorizer = new AuthorizeWebRequest(_signatureGeneratorFactoryMock.Object, _consumerRepositoryMock.Object);
            var authResult = requestAuthorizer.Authorize(_requestMock.Object);

            Assert.AreEqual(authResult, AuthorizationResult.Expired);
        }

        [TestMethod]
        public void Authorize_FailsWhenTimestampIsTooOld()
        {
            _signatureGeneratorFactoryMock.Setup(x => x.GetGenerator(It.IsAny<string>())).Returns(new HmacSha1SignatureGenerator());

            var publicKey = "publicKey";
            var timestamp = DateTimeOffset.UtcNow.AddMinutes(-1).Ticks.ToString();
            var signatureMethod = SignatureGeneratorNames.HmacSha1;
            var privateKey = "privateKey";
            var signature = _signatureGeneratorFactoryMock.Object.GetGenerator(signatureMethod).Generate(privateKey, string.Join("&", publicKey, signatureMethod, timestamp));
            _consumerMock.SetupGet(x => x.PrivateKey).Returns(privateKey);
            _consumerRepositoryMock.Setup(x => x.GetConsumerByPublicKey(It.IsAny<string>())).Returns(_consumerMock.Object);

            var nvc = new NameValueCollection{ 
                { AuthorizationTokens.PublicKey, publicKey }, 
                { AuthorizationTokens.Signature, signature }, 
                { AuthorizationTokens.SignatureMethod, signatureMethod }, 
                { AuthorizationTokens.TimeStamp, timestamp } };

            _requestMock.SetupGet(x => x.Form).Returns(nvc);

            var requestAuthorizer = new AuthorizeWebRequest(_signatureGeneratorFactoryMock.Object, _consumerRepositoryMock.Object);
            var authResult = requestAuthorizer.Authorize(_requestMock.Object);

            Assert.AreEqual(authResult, AuthorizationResult.Expired);
        }

        [TestMethod]
        public void Authorize_PassesAuthorized()
        {
            _signatureGeneratorFactoryMock.Setup(x => x.GetGenerator(It.IsAny<string>())).Returns(new HmacSha1SignatureGenerator());

            var publicKey = "publicKey";
            var timestamp = DateTimeOffset.UtcNow.AddSeconds(-10).Ticks.ToString();
            var signatureMethod = SignatureGeneratorNames.HmacSha1;
            var privateKey = "privateKey";
            var signature = _signatureGeneratorFactoryMock.Object.GetGenerator(signatureMethod).Generate(privateKey, string.Join("&", publicKey, timestamp, signatureMethod));

            _consumerMock.SetupGet(x => x.PrivateKey).Returns(privateKey);

            _consumerRepositoryMock.Setup(x => x.GetConsumerByPublicKey(It.IsAny<string>())).Returns(_consumerMock.Object);

            var nvc = new NameValueCollection{ 
                { AuthorizationTokens.PublicKey, publicKey }, 
                { AuthorizationTokens.Signature, signature }, 
                { AuthorizationTokens.SignatureMethod, signatureMethod }, 
                { AuthorizationTokens.TimeStamp, timestamp } };

            _requestMock.SetupGet(x => x.Form).Returns(nvc);

            var requestAuthorizer = new AuthorizeWebRequest(_signatureGeneratorFactoryMock.Object, _consumerRepositoryMock.Object);
            var authResult = requestAuthorizer.Authorize(_requestMock.Object);

            Assert.AreEqual(authResult, AuthorizationResult.Success);
        }
    }
}
