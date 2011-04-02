using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TideScraper.Core.Models;

namespace TideScraper.Data
{
    public interface IAuthRepository
    {
        void SaveNonce(string consumerKey, string nonce, DateTime timestamp);
        
        Consumer GetConsumer(string consumerKey);
        void SaveConsumer(Consumer consumer);
        
        void SaveRequestToken(RequestToken token);
        RequestToken GetRequestToken(string token);

        AccessToken GetAccessToken(string token);
        void SaveAccessToken(AccessToken accessToken);
    }
}
