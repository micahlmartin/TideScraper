using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using TideScraper.Core;
using MongoDB.Bson;
using TideScraper.Core.Models;
using MongoDB.Driver.Builders;

namespace TideScraper.Data
{
    public class AuthRepository : IAuthRepository
    {
        public void SaveNonce(string consumerKey, string nonce, DateTime timestamp)
        {
            var server = MongoServer.Create(Settings.TidesMongoConnection);

            try
            {
                server.Connect();

                var collection = server[DatabaseNames.Authentication][AuthenticationCollectionNames.Nonce];
                collection.EnsureIndex(new IndexKeysDocument { { "ConsumerKey", 1 }, { "None", 1 }, { "TimeStamp", 1 } }, new IndexOptionsDocument { { "unique", true } });

                collection.Insert(new BsonDocument { { "ConsumerKey", consumerKey }, { "Nonce", nonce }, { "TimeStamp", timestamp } });
            }
            finally
            {
                server.Disconnect();
            }
        }

        public Consumer GetConsumer(string consumerKey)
        {
            var server = MongoServer.Create(Settings.TidesMongoConnection);

            try
            {
                server.Connect();

                var collection = server[DatabaseNames.Authentication][AuthenticationCollectionNames.Consumers];

                return collection.FindOneAs<Consumer>(Query.EQ("ConsumerKey", consumerKey));
            }
            finally
            {
                server.Disconnect();
            }
        }

        public void SaveConsumer(Consumer consumer)
        {
            var server = MongoServer.Create(Settings.TidesMongoConnection);

            try
            {
                server.Connect();

                var collection = server[DatabaseNames.Authentication][AuthenticationCollectionNames.Consumers];
                collection.EnsureIndex(new IndexKeysDocument { { "ConsumerKey", 1 } }, new IndexOptionsDocument { { "unique", true } });

                collection.Save(consumer);
            }
            finally
            {
                server.Disconnect();
            }
        }

        public void SaveRequestToken(RequestToken token)
        {
            var server = MongoServer.Create(Settings.TidesMongoConnection);

            try
            {
                server.Connect();

                var collection = server[DatabaseNames.Authentication][AuthenticationCollectionNames.RequestTokens];
                collection.EnsureIndex(new IndexKeysDocument { { "ConsumerKey", 1 }, {"Token", 1} });

                collection.Save(token);
            }
            finally
            {
                server.Disconnect();
            }
        }


        public RequestToken GetRequestToken(string token)
        {
            var server = MongoServer.Create(Settings.TidesMongoConnection);

            try
            {
                server.Connect();

                var collection = server[DatabaseNames.Authentication][AuthenticationCollectionNames.RequestTokens];

                return collection.FindOneAs<RequestToken>(Query.EQ("Token", token));
            }
            finally
            {
                server.Disconnect();
            }
        }


        public AccessToken GetAccessToken(string token)
        {
            var server = MongoServer.Create(Settings.TidesMongoConnection);

            try
            {
                server.Connect();

                var collection = server[DatabaseNames.Authentication][AuthenticationCollectionNames.AccessTokens];

                return collection.FindOneAs<AccessToken>(Query.EQ("Token", token));
            }
            finally
            {
                server.Disconnect();
            }
        }


        public void SaveAccessToken(AccessToken accessToken)
        {
            var server = MongoServer.Create(Settings.TidesMongoConnection);

            try
            {
                server.Connect();

                var collection = server[DatabaseNames.Authentication][AuthenticationCollectionNames.AccessTokens];
                collection.EnsureIndex(new IndexKeysDocument { { "ConsumerKey", 1 }, { "Token", 1 } });

                collection.Save(accessToken);
            }
            finally
            {
                server.Disconnect();
            }
        }
    }
}
