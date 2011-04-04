using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap;
using StructureMap.Configuration.DSL;
using System.Web.Mvc;
using TideScraper.Data;
using TideScraper.Services;
using TideScraper.Core;
using TideScraper.Web.Api.Security;
using TideScraper.Core.Security;

namespace TideScraper.Web.Api.StructureMap
{
    public class StructureMapBootStrapper : IBootstrapper
    {
        public void BootstrapStructureMap()
        {
            ObjectFactory.Configure(x => { x.AddRegistry<DefaultRegistry>(); });

            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
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

                //Auth
                For<IAuthorizeRequest>().Use<AuthorizeWebRequest>();
                For<ISignatureGeneratorFactory>().Use<SignatureGeneratorFactory>();
                For<IConsumerRepository>().Use<ConsumerRepository>();
            }
        }
    }
}