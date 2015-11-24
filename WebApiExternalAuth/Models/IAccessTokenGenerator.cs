using System.Collections.Generic;
using System.Security.Claims;

namespace WebApiExternalAuth.Models
{
    public interface IAccessTokenGenerator
    {
        string Generate(IEnumerable<Claim> claims);
    }
}