using Microsoft.Owin.Security.Twitter;
using Rabbit.Security;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApiExternalAuth.Providers
{
    public class CustomTwitterAuthenticationProvider : TwitterAuthenticationProvider
    {
        public override Task Authenticated(TwitterAuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim(Claims.ExternalAccessToken, context.AccessToken));
            context.Identity.AddClaim(new Claim(Claims.ExternalUserName, context.ScreenName));

            return base.Authenticated(context);
        }
    }
}