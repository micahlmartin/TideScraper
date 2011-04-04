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
    }
}