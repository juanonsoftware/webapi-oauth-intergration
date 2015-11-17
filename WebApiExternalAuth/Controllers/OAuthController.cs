using BrockAllen.MembershipReboot;
using Microsoft.AspNet.Identity;
using Rabbit.Security;
using Rabbit.Web.Owin;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Results;
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
        public IHttpActionResult Login(string provider)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, Request);
            }

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var loginData = _loginDataParser.Parse(claimsIdentity);

            if (!string.Equals(loginData.ProviderName, provider, StringComparison.InvariantCultureIgnoreCase))
            {
                Request.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, Request);
            }

            var claims = (claimsIdentity != null) ? claimsIdentity.Claims : Enumerable.Empty<Claim>();
            var token = _tokenGenerator.Generate(claims);

            var accountLinked = _userAccountService.GetByLinkedAccount(loginData.ProviderName, loginData.ProviderKey);
            var emailExists = _userAccountService.EmailExists(loginData.Email);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Acc-AccessToken", token);
            response.Headers.Add("Acc-IsLinked", (accountLinked != null).ToString());
            response.Headers.Add("Acc-EmailExists", emailExists.ToString());

            return new ResponseMessageResult(response);
        }
    }
}
