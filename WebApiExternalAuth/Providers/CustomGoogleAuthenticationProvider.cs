using Microsoft.Owin.Security.Google;
using Rabbit.Security;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApiExternalAuth.Providers
{
    public class CustomGoogleAuthenticationProvider : GoogleOAuth2AuthenticationProvider
    {
        public override Task Authenticated(GoogleOAuth2AuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim(Claims.ExternalAccessToken, context.AccessToken));
            context.Identity.AddClaim(new Claim(Claims.ExternalExpiresIn, context.ExpiresIn.ToString()));

            return base.Authenticated(context);
        }
    }
}