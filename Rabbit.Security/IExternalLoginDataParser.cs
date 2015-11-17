using System.Security.Claims;

namespace Rabbit.Security
{
    public interface IExternalLoginDataParser
    {
        void Parse(ClaimsIdentity identity, ref ExternalLoginData loginData);
    }
}