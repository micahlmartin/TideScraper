using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using TideScraper.Data;
using TideScraper.Core.Models;

namespace TideScraper.Services
{
    public class AccountService : IAccountService
    {
        private IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public ServiceResult<dynamic> CreateApplication(CreateApplicationDetail detail)
        {
            if (detail == null)
                return new ServiceResult<dynamic> { ErrorMessage = "Incomplete data." };

            var isValid = detail.Validate();
            if (!isValid)
                return new ServiceResult<dynamic> { ErrorMessage = string.Join("\n", detail.Errors) };

            var application = new Application
            {
                AccountId = detail.AccountId,
                ApplicationName = detail.ApplicationName,
                ApplicationUrl = detail.ApplicationUrl,
                PrivateKey = Guid.NewGuid().ToString("n"),
                PublicKey = Guid.NewGuid().ToString("n"),
                ApplicationId = Guid.NewGuid().ToString("n")
            };

            _accountRepository.CreateApplication(application);

            dynamic newAccount = new ExpandoObject();
            newAccount.ApplicationId = application.ApplicationId;
            newAccount.PublicKey = application.PublicKey;
            newAccount.PrivateKey = application.PrivateKey;

            return new ServiceResult<dynamic>
            {
                Success = true,
                Result = newAccount
            };
        }
    }
}
