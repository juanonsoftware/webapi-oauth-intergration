using System;
using System.Security.Claims;

namespace Rabbit.Security
{
    public class OAuthLoginDataParser : ILoginDataParser
    {
        private readonly IExternalLoginDataParserFactory _externalLoginDataParserFactory;

        public OAuthLoginDataParser(IExternalLoginDataParserFactory externalLoginDataParserFactory)
        {
            _externalLoginDataParserFactory = externalLoginDataParserFactory;
        }

        public ExternalLoginData Parse(ClaimsIdentity identity)
        {
            if (identity == null)
            {
                return null;
            }

            var nameClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
            if (nameClaim == null || String.IsNullOrEmpty(nameClaim.Issuer) || String.IsNullOrEmpty(nameClaim.Value))
            {
                throw new ApplicationException("Cannot find a claim of ClaimTypes.NameIdentifier");
            }

            if (nameClaim.Issuer == ClaimsIdentity.DefaultIssuer)
            {
                return null;
            }

            var loginData = new ExternalLoginData
                {
                    ProviderName = nameClaim.Issuer,
                    ProviderKey = nameClaim.Value,
                    Name = identity.GetFirstOrDefault(ClaimTypes.Name),
                    Email = identity.GetFirstOrDefault(ClaimTypes.Email),
                };

            var externalParser = _externalLoginDataParserFactory.Create(loginData.ProviderName);
            externalParser.Parse(identity, ref loginData);

            return loginData;
        }
    }
}
