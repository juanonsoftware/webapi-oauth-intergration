using Microsoft.Owin.Security.MicrosoftAccount;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApiExternalAuth.Models;

namespace WebApiExternalAuth.Providers
{
    public class MicrosoftAccountCustomAuthenticationProvider : MicrosoftAccountAuthenticationProvider
    {
        public override Task Authenticated(MicrosoftAccountAuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim(Claims.ExternalAccessToken, context.AccessToken));
            context.Identity.AddClaim(new Claim(Claims.ExternalExpiresIn, context.ExpiresIn.ToString()));
            context.Identity.AddClaim(new Claim(Claims.ExternalEmail, context.Email));

            return base.Authenticated(context);
        }
    }
}