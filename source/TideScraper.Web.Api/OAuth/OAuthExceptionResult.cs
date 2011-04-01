using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OAuth.Core;
using System.Net;
using TideScraper.Core;

namespace TideScraper.Web.Api.OAuth
{
    public class OAuthExceptionResult : ActionResult
    {
        private readonly OAuthException _exception;

        /// <summary>
        /// Creates an ActionResult for the error specified in a particular <see cref="OAuthException" />.
        /// </summary>
        /// <param name="exception"></param>
        public OAuthExceptionResult(OAuthException exception)
        {
            _exception = exception;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;

            switch (_exception.Report.Problem)
            {

                case OAuthProblems.ParameterRejected:
                case OAuthProblems.SignatureMethodRejected:
                case OAuthProblems.ParameterAbset:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
            }

            response.AddHeader("WWW-Authenticate", string.Format("OAuth Realm=\"{0}\"", Settings.OAuthRealm));
            response.Write(_exception.Report.ToString());
        }
    }
}