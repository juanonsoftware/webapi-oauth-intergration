﻿using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin.Security;

namespace WebApiExternalAuth.Models
{
    public class ChallengeResult : IHttpActionResult
    {
        public ChallengeResult(string provider, string redirectUri, HttpRequestMessage request)
        {
            AuthenticationProvider = provider;
            RedirectUri = redirectUri;
            MessageRequest = request;
        }

        public string AuthenticationProvider { get; private set; }

        public string RedirectUri { get; private set; }

        public HttpRequestMessage MessageRequest { get; private set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var properties = new AuthenticationProperties()
            {
                RedirectUri = RedirectUri,
            };

            MessageRequest.GetOwinContext().Authentication.Challenge(properties, AuthenticationProvider);

            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                RequestMessage = MessageRequest
            };

            return Task.FromResult(response);
        }
    }
}
