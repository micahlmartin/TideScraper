using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TideScraper.Core.Security
{
    public enum AuthorizationResult
    {
        BadSignature,
        NonExistantConsumer,
        MissingData,
        Expired,
        Success
    }
}