using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using WebApiExternalAuth.Configuration;
using WebApiExternalAuth.Models;
using WebApiExternalAuth.Results;

namespace WebApiExternalAuth.Controllers
{
    public class OAuthController : ApiController
    {
        [HttpGet]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        public IHttpActionResult Login(string provider, string returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Url.Link("DefaultApi", new { controller = "Values" });
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, Request);
            }

            if (User.Identity.IsAuthenticated)
            {
                var loginData = LoginDataParser.Parse(User.Identity as ClaimsIdentity);

                if (!string.Equals(loginData.LoginProvider, provider, StringComparison.InvariantCultureIgnoreCase))
                {
                    Request.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    return new ChallengeResult(provider, Request);
                }
            }

            var tokenObject = GenerateLocalAccessTokenResponse(User.Identity.Name);

            var data = LoginDataParser.Parse(User.Identity as ClaimsIdentity);

            var sb = new StringBuilder();
            sb.Append(returnUrl);
            sb.AppendFormat("#access_token={0}&provider={1}&name={2}", tokenObject["access_token"], data.LoginProvider, data.Name);

            return Redirect(sb.ToString());
        }

        private JObject GenerateLocalAccessTokenResponse(string userName)
        {
            var tokenExpiration = TimeSpan.FromDays(1);

            var identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);

            identity.AddClaim(new Claim(ClaimTypes.Name, userName));
            identity.AddClaim(new Claim("role", "user"));

            var props = new AuthenticationProperties()
            {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
            };

            var ticket = new AuthenticationTicket(identity, props);

            var accessToken = SecurityConfig.OAuthBearerOptions.AccessTokenFormat.Protect(ticket);

            JObject tokenResponse = new JObject(
                new JProperty("userName", userName),
                new JProperty("access_token", accessToken),
                new JProperty("token_type", "bearer"),
                new JProperty("expires_in", tokenExpiration.TotalSeconds.ToString()),
                new JProperty(".issued", ticket.Properties.IssuedUtc.ToString()),
                new JProperty(".expires", ticket.Properties.ExpiresUtc.ToString())
                );

            return tokenResponse;
        }
    }
}