using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TideScraper.Core.Security
{
    public class SignatureGeneratorFactory : ISignatureGeneratorFactory
    {
        public ISignatureGenerator GetGenerator(string name)
        {
            switch (name)
            {
                case SignatureGeneratorNames.HmacSha1:
                    return new HmacSha1SignatureGenerator();
                default:
                    return null;
            }
        }
    }
}
