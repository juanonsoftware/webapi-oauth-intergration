using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.Relational;
using Microsoft.AspNet.Identity;
using Rabbit.Security;
using Rabbit.Web.Owin;
using System;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using WebApiExternalAuth.Models;

namespace WebApiExternalAuth.Controllers
{
    public class OAuthController : ApiController
    {
        private readonly ILoginDataParser _loginDataParser;
        private readonly AccessTokenGenerator _tokenGenerator;
        private readonly UserAccountService _userAccountService;

        public OAuthController(ILoginDataParser loginDataParser, AuthenticationService authenticationService, AccessTokenGenerator tokenGenerator)
        {
            _loginDataParser = loginDataParser;
            _tokenGenerator = tokenGenerator;
            _userAccountService = authenticationService.UserAccountService;
        }

        [HttpGet]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        public IHttpActionResult Login(string provider, string returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Url.Link("DefaultApi", new { controller = typeof(ValuesController).GetControllerName() });
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, Request);
            }

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var loginData = _loginDataParser.Parse(claimsIdentity);
            claimsIdentity.TryBuildCustomData(ref loginData);

            if (User.Identity.IsAuthenticated)
            {
                if (!string.Equals(loginData.ProviderName, provider, StringComparison.InvariantCultureIgnoreCase))
                {
                    Request.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    return new ChallengeResult(provider, Request);
                }
            }

            var userAccount = (RelationalUserAccount)_userAccountService.GetByEmail(loginData.Email);

            if (userAccount == null)
            {
                var userName = loginData.GetUserName();
                userAccount = (RelationalUserAccount)_userAccountService.CreateAccount(userName, Guid.NewGuid().ToString(), loginData.Email);
            }

            var token = _tokenGenerator.Generate(new Claim[]
            {
                new Claim(ClaimTypes.Email, loginData.Email),
                new Claim(ClaimTypes.Name, loginData.Name),
            });

            var sb = new StringBuilder();
            sb.Append(returnUrl);
            sb.AppendFormat("#access_token={0}&provider={1}&name={2}", token, loginData.ProviderName, loginData.Name);

            return Redirect(sb.ToString());
        }
    }
}
