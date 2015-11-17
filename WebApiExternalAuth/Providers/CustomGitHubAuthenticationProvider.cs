using Owin.Security.Providers.GitHub;
using Rabbit.Security;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApiExternalAuth.Providers
{
    public class CustomGitHubAuthenticationProvider : GitHubAuthenticationProvider
    {
        public override Task Authenticated(GitHubAuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim(Claims.ExternalAccessToken, context.AccessToken));
            context.Identity.AddClaim(new Claim(Claims.ExternalUserName, context.Name));

            return base.Authenticated(context);
        }
    }
}