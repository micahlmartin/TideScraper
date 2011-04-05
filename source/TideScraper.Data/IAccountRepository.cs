using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TideScraper.Core.Models;

namespace TideScraper.Data
{
    public interface IAccountRepository
    {
        void CreateApplication(Application application);

        Account GetAccountById(string accountId);
    }
}
