using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TideScraper.Core.Security;

namespace TideScraper.Core.Security
{
    public class AuthorizeWebRequest : IAuthorizeRequest
    {
        private ISignatureGeneratorFactory _signatureGeneratorFactory;
        private IConsumerRepository _consumerRepository;

        public AuthorizeWebRequest(ISignatureGeneratorFactory signatureGeneratorFactory, IConsumerRepository consumerRepository)
        {
            _signatureGeneratorFactory = signatureGeneratorFactory;
            _consumerRepository = consumerRepository;
        }

        public AuthorizationResult Authorize(HttpRequestBase request)
        {
            var requestData = new AuthorizeRequestData(request.Form);

            if (!VerifyDataIsIntact(requestData))
                return AuthorizationResult.MissingData;

            if (!VerifyTimestamp(requestData.Timestamp))
                return AuthorizationResult.Expired;

            var consumer = _consumerRepository.GetConsumerByPublicKey(requestData.PublicKey);
            if (consumer == null)
                return AuthorizationResult.NonExistantConsumer;

            if (!VerifySignature(consumer.PrivateKey, requestData))
                return AuthorizationResult.BadSignature;

            return AuthorizationResult.Success;
        }

        private bool VerifyTimestamp(string timestamp)
        {
            long ticks;
            if (!long.TryParse(timestamp, out ticks))
                return false;

            var time = new DateTimeOffset(ticks, TimeSpan.Zero);
            var now = DateTimeOffset.UtcNow;

            if (time > now)
                return false;
            if ((now - time) > TimeSpan.FromMinutes(1))
                return false;

            return true;
        }

        private bool VerifySignature(string privateKey, AuthorizeRequestData requestData)
        {
            var signatureGenerator = _signatureGeneratorFactory.GetGenerator(requestData.SignatureMethod);
            var signature = requestData.Signature;

            var data = requestData.GetDataForSignature();

            var hash = signatureGenerator.Generate(privateKey, data);

            if (hash == signature)
                return true;

            return false;
        }

        private bool VerifyDataIsIntact(AuthorizeRequestData data)
        {
            if (string.IsNullOrWhiteSpace(data.PublicKey) ||
                string.IsNullOrWhiteSpace(data.Signature) ||
                string.IsNullOrWhiteSpace(data.SignatureMethod) ||
                string.IsNullOrWhiteSpace(data.Timestamp))
                return false;

            return true;

        }
    }
}