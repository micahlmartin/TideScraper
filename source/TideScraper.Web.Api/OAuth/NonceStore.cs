using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OAuth.Core.Storage.Interfaces;
using OAuth.Core.Interfaces;
using TideScraper.Data;
using StructureMap;
using MongoDB.Driver;

namespace TideScraper.Web.Api.OAuth
{
    public class NonceStore : INonceStore
    {
        private IAuthRepository _authRepository = ObjectFactory.GetInstance<IAuthRepository>();

        public NonceStore(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public bool RecordNonceAndCheckIsUnique(IConsumer consumer, string nonce)
        {
            try
            {
                _authRepository.SaveNonce(consumer.ConsumerKey, nonce, DateTime.Now);
                return true;
            }
            catch (MongoException)
            {
                return true;
            }
        }
    }
}