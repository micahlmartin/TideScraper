using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OAuth.Core.Storage.Interfaces;
using System.Security.Cryptography.X509Certificates;
using OAuth.Core.Interfaces;
using TideScraper.Data;
using StructureMap;

namespace TideScraper.Web.Api.OAuth
{
    public class ConsumerStore : IConsumerStore
    {
        private IAuthRepository _authRepository = ObjectFactory.GetInstance<IAuthRepository>();

        public ConsumerStore(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public X509Certificate2 GetConsumerCertificate(IConsumer consumer)
        {
            throw new NotImplementedException();
        }

        public string GetConsumerSecret(IConsumer consumer)
        {
            return _authRepository.GetConsumer(consumer.ConsumerKey).ConsumerSecret;
        }

        public bool IsConsumer(IConsumer consumer)
        {
            return _authRepository.GetConsumer(consumer.ConsumerKey) != null;
        }

        public void SetConsumerCertificate(IConsumer consumer, X509Certificate2 certificate)
        {
            throw new NotImplementedException();
        }

        public void SetConsumerSecret(IConsumer consumer, string consumerSecret)
        {
            var dbConsumer = _authRepository.GetConsumer(consumer.ConsumerKey);
            dbConsumer.ConsumerSecret = consumerSecret;
            _authRepository.SaveConsumer(dbConsumer);
        }
    }
}