using Owin.Security.Providers.LinkedIn;
using Rabbit.Security;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApiExternalAuth.Providers
{
    public class CustomLinkedInAuthenticationProvider : LinkedInAuthenticationProvider
    {
        public override Task Authenticated(LinkedInAuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim(Claims.ExternalExpiresIn, context.ExpiresIn.ToString()));

            return base.Authenticated(context);
        }
    }
}
