using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TideScraper.Core.Security
{
    public interface ISignatureGeneratorFactory
    {
        ISignatureGenerator GetGenerator(string name);
    }
}
