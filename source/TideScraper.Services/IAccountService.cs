using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TideScraper.Services
{
    public interface IAccountService
    {
        ServiceResult<dynamic> CreateApplication(CreateApplicationDetail detail);
    }
}
