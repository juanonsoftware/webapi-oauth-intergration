using Rabbit.Security;
using System.Security.Claims;

namespace WebApiExternalAuth.Models
{
    public static class ClaimsIdentityExtensions
    {
        public static void TryBuildCustomData(this ClaimsIdentity identity, ref ExternalLoginData loginData)
        {
            if (Providers.Facebook.ToString().Equals(loginData.ProviderName))
            {
                loginData.Profile = "https://www.facebook.com/" + loginData.ProviderKey;
                loginData.Properties.Add(Claims.ExternalExpiresIn, identity.FindFirst(Claims.ExternalExpiresIn).Value);
            }

            if (Providers.Google.ToString().Equals(loginData.ProviderName))
            {
                loginData.Profile = identity.FindFirst(Claims.ExternalProfile).Value;
            }

            if (Providers.GitHub.ToString().Equals(loginData.ProviderName))
            {
                loginData.Profile = "https://github.com/" + loginData.UserName;
            }
        }
    }
}
