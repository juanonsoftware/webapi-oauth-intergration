using System.Security.Claims;

namespace Rabbit.Security
{
    public static class ClaimsIdentityExtensions
    {
        public static string GetFirstOrDefault(this ClaimsIdentity identity, string claimType)
        {
            var claim = identity.FindFirst(claimType);
            return (claim == null) ? string.Empty : claim.Value;
        }
    }
}