using Rabbit.Security;
using System.Security.Claims;

namespace WebApiExternalAuth.Models
{
    public class GoogleClaimsParser : IExternalLoginDataParser
    {
        public void Parse(ClaimsIdentity identity, ref ExternalLoginData loginData)
        {
            loginData.Profile = identity.GetFirstOrDefault("urn:google:profile");
        }
    }
}