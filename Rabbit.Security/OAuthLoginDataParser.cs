using System;
using System.Security.Claims;

namespace Rabbit.Security
{
    public class OAuthLoginDataParser : ILoginDataParser
    {
        public ExternalLoginData Parse(ClaimsIdentity identity)
        {
            if (identity == null)
            {
                return null;
            }

            var nameClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

            if (nameClaim == null || String.IsNullOrEmpty(nameClaim.Issuer) || String.IsNullOrEmpty(nameClaim.Value))
            {
                throw new ApplicationException("Can not find a claim of ClaimTypes.NameIdentifier");
            }

            if (nameClaim.Issuer == ClaimsIdentity.DefaultIssuer)
            {
                return null;
            }

            var loginData = new ExternalLoginData
                {
                    ProviderName = nameClaim.Issuer,
                    ProviderKey = nameClaim.Value,
                    UserName = identity.FindFirst(ClaimTypes.Name).Value,
                    Name = identity.FindFirst(Claims.ExternalName).Value,
                    Email = identity.FindFirst(ClaimTypes.Email).Value,
                    ExternalAccessToken = identity.FindFirst(Claims.ExternalAccessToken).Value,
                };

            return loginData;
        }
    }
}