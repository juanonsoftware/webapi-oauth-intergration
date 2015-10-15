using System.Security.Claims;
using System.Threading.Tasks;
using Owin.Security.Providers.Yahoo;

namespace WebApiExternalAuth.Models
{
    public class YahooCustomAuthenticationProvider : YahooAuthenticationProvider
    {
        public override Task Authenticated(YahooAuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim(ClaimTypes.Email, context.Email));

            return base.Authenticated(context);
        }
    }
}