using Rabbit.Security;
using System.Security.Claims;

namespace WebApiExternalAuth.Models
{
    public class TwitterClaimsParser : IExternalLoginDataParser
    {
        public void Parse(ClaimsIdentity identity, ref ExternalLoginData loginData)
        {
        }
    }
}