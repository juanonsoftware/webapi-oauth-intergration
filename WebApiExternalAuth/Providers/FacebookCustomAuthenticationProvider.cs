using Microsoft.Owin.Security.Facebook;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApiExternalAuth.Models;

namespace WebApiExternalAuth.Providers
{
    public class FacebookCustomAuthenticationProvider : FacebookAuthenticationProvider
    {
        public override Task Authenticated(FacebookAuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim(Claims.ExternalAccessToken, context.AccessToken));
            context.Identity.AddClaim(new Claim(Claims.ExternalExpiresIn, context.ExpiresIn.ToString()));

            return base.Authenticated(context);
        }
    }
}