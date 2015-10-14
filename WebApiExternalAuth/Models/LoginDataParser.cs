using System;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

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
                    Email = identity.FindFirstValue(ClaimTypes.Email),
                    ExternalAccessToken = identity.FindFirstValue(Claims.ExternalAccessToken),
                };

            TryBuildProfile(loginData, identity);

            return loginData;
        }

        private static void TryBuildProfile(LoginData loginData, ClaimsIdentity identity)
        {
            if (Providers.Facebook.ToString().Equals(loginData.LoginProvider, StringComparison.InvariantCultureIgnoreCase))
            {
                loginData.Profile = "https://www.facebook.com/" + loginData.ProviderKey;
                return;
            }

            if (Providers.Google.ToString().Equals(loginData.LoginProvider, StringComparison.InvariantCultureIgnoreCase))
            {
                loginData.Profile = identity.FindFirstValue(Claims.ExternalProfile);
                return;
            }
        }
    }
}