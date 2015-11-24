using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace WebApiExternalAuth.Models
{
    public class AccessTokenGenerator : IAccessTokenGenerator
    {
        private readonly OAuthBearerAuthenticationOptions _authenticationOptions;

        public AccessTokenGenerator(OAuthBearerAuthenticationOptions authenticationOptions)
        {
            _authenticationOptions = authenticationOptions;
        }

        public string Generate(IEnumerable<Claim> claims)
        {
            var identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);
            identity.AddClaims(claims);

            var tokenExpiration = TimeSpan.FromDays(1);

            var props = new AuthenticationProperties()
            {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
                AllowRefresh = true,
            };
            var ticket = new AuthenticationTicket(identity, props);

            return _authenticationOptions.AccessTokenFormat.Protect(ticket);
        }
    }
}