using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Twitter;
using Rabbit.Security;

namespace WebApiExternalAuth.Providers
{
    public class TwitterCustomAuthenticationProvider : TwitterAuthenticationProvider
    {
        public override Task Authenticated(TwitterAuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim(Claims.ExternalAccessToken, context.AccessToken));
            context.Identity.AddClaim(new Claim(Claims.ExternalName, context.ScreenName));

            return base.Authenticated(context);
        }
    }
}