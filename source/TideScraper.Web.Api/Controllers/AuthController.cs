using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TideScraper.Data;
using TideScraper.Core.Models;
using TideScraper.Core;

namespace TideScraper.Web.Api.Controllers
{
    public class AuthController : Controller
    {
        private IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public ActionResult AuthorizeRequestToken(string oauth_token, string oauth_callback)
        {
            var requestToken = _authRepository.GetRequestToken(oauth_token);
            if (requestToken != null)
            {
                requestToken.IsAccessDenied = false;
                var accessToken = new AccessToken
                                    {
                                        ConsumerKey = requestToken.ConsumerKey,
                                        Expiration = DateTime.UtcNow.AddDays(Settings.OAuthExpirationDays),
                                        Realm = requestToken.Realm,
                                        Token = Guid.NewGuid().ToString(),
                                        TokenSecret = Guid.NewGuid().ToString(),
                                        UserName = Guid.NewGuid().ToString(),
                                    };
                requestToken.AccessToken = accessToken;
                _authRepository.SaveAccessToken(accessToken);
                _authRepository.SaveRequestToken(requestToken);
                return new JsonResult { Data = accessToken };
            }
            return new EmptyResult();

        }
    }
}