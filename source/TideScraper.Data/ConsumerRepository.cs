using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TideScraper.Core.Security;
using MongoDB.Driver;
using TideScraper.Core;
using TideScraper.Core.Models;
using MongoDB.Driver.Builders;

namespace TideScraper.Data
{
    public class ConsumerRepository : IConsumerRepository
    {
        public IConsumer GetConsumerByPublicKey(string publicKey)
        {
            var server = MongoServer.Create(Settings.TidesMongoConnection);

            try
            {
                server.Connect();

                var collection = server[DatabaseNames.Authentication][AuthenticationCollectionNames.Consumers];
                collection.EnsureIndex(new IndexKeysDocument { { "ConsumerKey", 1 } });

                return collection.FindOneAs<Consumer>(Query.EQ("ConsumerKey", publicKey)) as IConsumer;
            }
            catch (MongoException)
            {
                return null;
            }
            finally
            {
                server.Disconnect();
            }
        }
    }
}
