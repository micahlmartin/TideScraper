using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StructureMap;
using TideScraper.Core.Security;

namespace TideScraper.Web.Api.Security
{
    public class RequireAuthAttribute : AuthorizeAttribute
    {
        private AuthorizationResult _authorizationResult;

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authorizer = ObjectFactory.GetInstance<IAuthorizeRequest>();
            _authorizationResult = authorizer.Authorize(httpContext.Request);
            return _authorizationResult == AuthorizationResult.Success;            
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            switch (_authorizationResult)
            {
                case AuthorizationResult.MissingData:
                case AuthorizationResult.BadSignature:
                    filterContext.Result = new HttpStatusCodeResult(400);
                    break;
                case AuthorizationResult.NonExistantConsumer:
                case AuthorizationResult.Expired:
                    filterContext.Result = new HttpStatusCodeResult(401);
                    break;
            }
        }

        private IAuthorizeRequest _requestAuthorizer;
        public IAuthorizeRequest RequestAuthorizer
        {
            get
            {
                if (_requestAuthorizer == null)
                    _requestAuthorizer = ObjectFactory.GetInstance<IAuthorizeRequest>();

                return _requestAuthorizer;
            }
            set { _requestAuthorizer = value; }
        }
    }
}