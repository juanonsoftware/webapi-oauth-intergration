using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using WebApiExternalAuth.Configuration;
using WebApiExternalAuth.Models;

namespace WebApiExternalAuth.Controllers
{
    public class AuthController : ApiController
    {
        [HttpGet]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        public dynamic Finish(string returnUrl = null)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Not authenticated");
            }


            var obj = GenerateLocalAccessTokenResponse(User.Identity.Name);

            var data = LoginDataParser.Parse(User.Identity as ClaimsIdentity);

            var sb = new StringBuilder();
            sb.Append(returnUrl);
            sb.AppendFormat("#ex_access_token={0}&provider={1}&name={2}&access_token={3}", data.ExternalAccessToken, data.LoginProvider, data.Name, obj["access_token"]);

            return Redirect(sb.ToString());
        }

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
                var continueUrl = Url.Link("ApiAction", new { controller = this.GetControllerName(), action = "Finish", returnUrl = returnUrl });
                return new ChallengeResult(provider, continueUrl, Request);
            }

            var obj = GenerateLocalAccessTokenResponse(User.Identity.Name);

            var data = LoginDataParser.Parse(User.Identity as ClaimsIdentity);

            var sb = new StringBuilder();
            sb.Append(returnUrl);
            sb.AppendFormat("#ex_access_token={0}&provider={1}&name={2}&access_token={3}", data.ExternalAccessToken, data.LoginProvider, data.Name, obj["access_token"]);

            return Redirect(sb.ToString());
        }

        private JObject GenerateLocalAccessTokenResponse(string userName)
        {
            var tokenExpiration = TimeSpan.FromDays(1);

            ClaimsIdentity identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);

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