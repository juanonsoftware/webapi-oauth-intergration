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

        public static string GetEmail(this ClaimsIdentity identity)
        {
            return identity.GetFirstOrDefault(ClaimTypes.Email);
        }
    }
}