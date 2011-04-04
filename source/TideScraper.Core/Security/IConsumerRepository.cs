using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TideScraper.Core.Security
{
    public interface IConsumerRepository
    {
        IConsumer GetConsumerByPublicKey(string publicKey);
    }
}