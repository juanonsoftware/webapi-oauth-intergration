using Microsoft.AspNet.Identity;
using Rabbit.Security;
using System.Security.Claims;

namespace WebApiExternalAuth.Models
{
    public class TwitterClaimsParser : IExternalLoginDataParser
    {
        public void Parse(ClaimsIdentity identity, ref ExternalLoginData loginData)
        {
            loginData.UserName = identity.GetUserName();
            loginData.Profile = string.Format("https://twitter.com/{0}", loginData.UserName);
        }
    }
}