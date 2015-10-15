using Microsoft.AspNet.Identity;
using System;
using System.Security.Claims;

namespace WebApiExternalAuth.Models
{
    public static class LoginDataParser
    {
        public static LoginData Parse(ClaimsIdentity identity)
        {
            if (identity == null)
            {
                return null;
            }

            Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

            if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer) || String.IsNullOrEmpty(providerKeyClaim.Value))
            {
                return null;
            }

            if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
            {
                return null;
            }

            var loginData = new LoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name),
                    Name = identity.FindFirstValue(Claims.ExternalName),
                    Email = identity.FindFirstValue(ClaimTypes.Email),
                    ExternalAccessToken = identity.FindFirstValue(Claims.ExternalAccessToken),
                };

            TryBuildCustomData(loginData, identity);

            return loginData;
        }

        private static void TryBuildCustomData(LoginData loginData, ClaimsIdentity identity)
        {
            if (Providers.Facebook.ToString().Equals(loginData.LoginProvider, StringComparison.InvariantCultureIgnoreCase))
            {
                loginData.Profile = "https://www.facebook.com/" + loginData.ProviderKey;
                loginData.Properties.Add(Claims.ExternalExpiresIn, identity.FindFirstValue(Claims.ExternalExpiresIn));
                return;
            }

            if (Providers.Google.ToString().Equals(loginData.LoginProvider, StringComparison.InvariantCultureIgnoreCase))
            {
                loginData.Profile = identity.FindFirstValue(Claims.ExternalProfile);
                return;
            }

            if (Providers.GitHub.ToString().Equals(loginData.LoginProvider, StringComparison.InvariantCultureIgnoreCase))
            {
                loginData.Profile = "https://github.com/" + loginData.UserName;
                return;
            }
        }
    }
}
