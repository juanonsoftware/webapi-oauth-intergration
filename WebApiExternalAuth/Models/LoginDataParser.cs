using Rabbit.Security;

namespace WebApiExternalAuth.Models
{
    public static class ClaimsIdentityExtensions
    {
        public static string GetUserName(this ExternalLoginData loginData)
        {
            return string.Format("{0}_{1}", loginData.ProviderName, loginData.ProviderKey);
        }
    }
}
