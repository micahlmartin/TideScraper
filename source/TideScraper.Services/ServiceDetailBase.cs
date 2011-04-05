using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using TideScraper.Core;

namespace TideScraper.Services
{
    public abstract class ServiceDetailBase : IValidate
    {
        private IList<ValidationError> _errors = new List<ValidationError>();

        protected void SetError(string propertyName, string message)
        {
            _errors.Add(new ValidationError
            {
                ErrorMessage = message,
                PropertyName = propertyName
            });
        }

        public bool Validate()
        {
            _errors.Clear();

            OnValidate();

            return !_errors.Any();
        }

        protected abstract void OnValidate();

        public IEnumerable<ValidationError> Errors { get { return _errors; } }
    }
}
