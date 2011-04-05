using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TideScraper.Core
{
    public interface IValidate
    {
        bool Validate();
        IEnumerable<ValidationError> Errors { get; }
    }
}
