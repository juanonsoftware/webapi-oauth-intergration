using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Google;
using Rabbit.Security;

namespace WebApiExternalAuth.Providers
{
    public class GoogleCustomAuthenticationProvider : GoogleOAuth2AuthenticationProvider
    {
        public override Task Authenticated(GoogleOAuth2AuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim(Claims.ExternalAccessToken, context.AccessToken));
            context.Identity.AddClaim(new Claim(Claims.ExternalProfile, context.Profile));

            return base.Authenticated(context);
        }
    }
}