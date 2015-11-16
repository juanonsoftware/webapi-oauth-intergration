using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.MicrosoftAccount;
using Rabbit.Security;

namespace WebApiExternalAuth.Providers
{
    public class MicrosoftAccountCustomAuthenticationProvider : MicrosoftAccountAuthenticationProvider
    {
        public override Task Authenticated(MicrosoftAccountAuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim(Claims.ExternalAccessToken, context.AccessToken));
            context.Identity.AddClaim(new Claim(Claims.ExternalUserName, context.Name));

            if (context.ExpiresIn.HasValue)
            {
                context.Identity.AddClaim(new Claim(Claims.ExternalExpiresIn, context.ExpiresIn.ToString()));
            }

            return base.Authenticated(context);
        }
    }
}