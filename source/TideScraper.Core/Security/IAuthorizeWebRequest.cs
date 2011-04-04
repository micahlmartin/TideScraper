using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TideScraper.Core.Security
{
    public interface IAuthorizeRequest
    {
        AuthorizationResult Authorize(HttpRequestBase request);
    }
}
