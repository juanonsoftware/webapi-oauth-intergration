using System.Security.Claims;

namespace Rabbit.Security
{
    public interface ILoginDataParser
    {
        ExternalLoginData Parse(ClaimsIdentity identity);
    }
}
