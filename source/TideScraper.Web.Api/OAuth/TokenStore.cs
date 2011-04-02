using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OAuth.Core.Storage.Interfaces;
using OAuth.Core.Interfaces;
using OAuth.Core.Storage;
using TideScraper.Core.Models;
using TideScraper.Data;
using OAuth.Core;

namespace TideScraper.Web.Api.OAuth
{
    public class TokenStore : ITokenStore
    {
        private IAuthRepository _authRepository;

        public TokenStore(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public void ConsumeAccessToken(IOAuthContext accessContext)
        {
            AccessToken accessToken = _authRepository.GetAccessToken(accessContext.Token);

            if (accessToken.Expiration < DateTime.Now)
            {
                throw new OAuthException(accessContext, OAuthProblems.TokenExpired, "Token has expired (they're only valid for 1 minute)");
            }
        }

        public void ConsumeRequestToken(IOAuthContext requestContext)
        {
            var requestToken = _authRepository.GetRequestToken(requestContext.Token);

            if (requestToken.IsExpired)
                throw new OAuthException(requestContext, OAuthProblems.TokenRejected, "The request token has already be consumed.");
            
            if (!requestToken.IsAccessDenied)
                requestToken.IsExpired = true;

            _authRepository.SaveRequestToken(requestToken);
        }

        public IToken CreateRequestToken(IOAuthContext context)
        {
            var token = new RequestToken
            {
                ConsumerKey = context.ConsumerKey,
                Realm = context.Realm,
                Token = Guid.NewGuid().ToString("n"),
                TokenSecret = Guid.NewGuid().ToString("n"),
                IsAccessDenied = true
            };

            _authRepository.SaveRequestToken(token);

            return token;
        }

        public IToken GetAccessTokenAssociatedWithRequestToken(IOAuthContext requestContext)
        {
            RequestToken request = _authRepository.GetRequestToken(requestContext.Token);
            return request.AccessToken;
        }

        public RequestForAccessStatus GetStatusOfRequestForAccess(IOAuthContext accessContext)
        {
            RequestToken request = _authRepository.GetRequestToken(accessContext.Token);

            if (request.IsAccessDenied) return RequestForAccessStatus.Denied;

            if (request.AccessToken == null) return RequestForAccessStatus.Unknown;

            return RequestForAccessStatus.Granted;
        }

        public IToken GetToken(IOAuthContext context)
        {
            var token = (IToken)null;
            if (!string.IsNullOrEmpty(context.Token))
            {
                token = _authRepository.GetAccessToken(context.Token) ??
                        (IToken)_authRepository.GetRequestToken(context.Token);
            }
            return token;
        }
    }
}