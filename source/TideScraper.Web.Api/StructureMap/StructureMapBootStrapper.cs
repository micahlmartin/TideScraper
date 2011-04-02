using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap;
using StructureMap.Configuration.DSL;
using System.Web.Mvc;
using TideScraper.Web.Api.OAuth;
using OAuth.Core.Storage.Interfaces;
using TideScraper.Data;
using TideScraper.Services;
using OAuth.Core.Interfaces;
using OAuth.Core;
using OAuth.Core.Provider;
using OAuth.Core.Provider.Inspectors;
using TideScraper.Core;

namespace TideScraper.Web.Api.StructureMap
{
    public class StructureMapBootStrapper : IBootstrapper
    {
        public void BootstrapStructureMap()
        {
            ObjectFactory.Configure(x => { x.AddRegistry<DefaultRegistry>(); });

            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
            ModelBinders.Binders.Add(typeof(OAuthBinder), ObjectFactory.GetInstance<OAuthBinder>());
        }

        public class DefaultRegistry : Registry
        {
            public DefaultRegistry()
            {
                //Data
                For<IAuthRepository>().Use<AuthRepository>();
                For<ITideRepository>().Use<MongoTideRepository>();

                //Services
                For<IGeoService>().Use<GeoService>();
                For<IScraperService>().Use<ScraperService>();
                For<ITideService>().Use<TideService>();

                //OAuth
                For<IConsumerStore>().Use<ConsumerStore>();
                For<INonceStore>().Use<NonceStore>();
                For<ITokenStore>().Use<TokenStore>();
                For<IOAuthContextBuilder>().Use<OAuthContextBuilder>();

                var oauthProvider =
                For<IOAuthProvider>().Singleton().Use(x =>
                {
                    return new OAuthProvider(x.GetInstance<ITokenStore>(), new ConsumerValidationInspector(x.GetInstance<IConsumerStore>()), new NonceStoreInspector(x.GetInstance<INonceStore>()), new TimestampRangeInspector(TimeSpan.FromMinutes(Settings.OAuthTimeoutMinutes)), new SignatureValidationInspector(x.GetInstance<IConsumerStore>()));
                });
            }
        }
    }
}