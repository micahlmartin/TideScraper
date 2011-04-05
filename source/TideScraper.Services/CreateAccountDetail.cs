using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace TideScraper.Services
{
    public class CreateApplicationDetail : ServiceDetailBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationUrl { get; set; }
        public string AccountId { get; set; }

        protected override void OnValidate()
        {
            if (string.IsNullOrWhiteSpace(FirstName))
                SetError("FirstName", "First name is required.");
            if (string.IsNullOrWhiteSpace(LastName))
                SetError("LastName", "Last name is required.");
            if (string.IsNullOrWhiteSpace(EmailAddress))
                SetError("EmailAddress", "Email address is required.");
            else if (!EmailRegex.IsMatch(EmailAddress))
                SetError("EmailAddress", "Invalid email address.");
            if (string.IsNullOrWhiteSpace(ApplicationName))
                SetError("ApplicationName", "Application Name is required.");
            if (string.IsNullOrWhiteSpace(ApplicationUrl))
                SetError("ApplicationUrl", "Application Url is required.");
            if (string.IsNullOrWhiteSpace(AccountId))
                SetError("AccountId", "Account Id is required.");
        }

        private static Regex EmailRegex = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
    }
}
