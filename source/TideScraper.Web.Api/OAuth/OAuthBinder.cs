using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OAuth.Core.Interfaces;

namespace TideScraper.Web.Api.OAuth
{
    public class OAuthBinder : IModelBinder
    {
        public OAuthBinder(IOAuthContextBuilder contextBuilder)
        {
            OAuthContextBuilder = contextBuilder;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (OAuthContextBuilder == null)
                throw new NullReferenceException("OAuthContextBuilder");

            if (bindingContext.ModelType.IsAssignableFrom(typeof(IOAuthContext)))
                return OAuthContextBuilder.FromHttpRequest(controllerContext.HttpContext.Request);

            return null;
        }

        public IOAuthContextBuilder OAuthContextBuilder { get; set; }
    }
}