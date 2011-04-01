using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OAuth.Core.Interfaces;
using System.Web.Mvc;
using OAuth.Core;
using StructureMap;
using TideScraper.Core;

namespace TideScraper.Web.Api.OAuth
{
    public class OAuthSecuredAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        public OAuthSecuredAttribute()
        {
            OAuthContextBuilder = ObjectFactory.GetInstance<IOAuthContextBuilder>();
            OAuthProvider = ObjectFactory.GetInstance<IOAuthProvider>();
        }

        public IOAuthContextBuilder OAuthContextBuilder { get; set; }
        public IOAuthProvider OAuthProvider { get; set; }

        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (OAuthContextBuilder == null)
                throw new NullReferenceException("OAuthContextBuilder wasn't set in the Authorisation filter, please use an IOC container to do this");
            if (OAuthProvider == null)
                throw new NullReferenceException("OAuthProvider wasn't set in the Authorisation filter, please use an IOC container to do this");
            try
            {
                var context = OAuthContextBuilder.FromHttpRequest(filterContext.HttpContext.Request);
                OAuthProvider.AccessProtectedResourceRequest(context);
            }
            catch (OAuthException ex)
            {

                filterContext.Result = new OAuthExceptionResult(ex);
            }

        }
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var response = filterContext.HttpContext.Response;
            var header = string.Format("OAuth Realm=\"{0}\"", Settings.OAuthRealm);
            response.AddHeader("WWW-Authenticate", header);
        }
    }
}