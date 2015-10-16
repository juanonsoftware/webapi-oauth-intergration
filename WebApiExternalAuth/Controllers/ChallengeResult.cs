using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApiExternalAuth.Controllers
{
    public class ChallengeResult : IHttpActionResult
    {
        public ChallengeResult(string provider, HttpRequestMessage requestMessage)
        {
            AuthenticationProvider = provider;
            RequestMessageMessage = requestMessage;
        }

        public string AuthenticationProvider { get; private set; }

        public HttpRequestMessage RequestMessageMessage { get; private set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            RequestMessageMessage.GetOwinContext().Authentication.Challenge(AuthenticationProvider);

            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                RequestMessage = RequestMessageMessage
            };

            return Task.FromResult(response);
        }
    }
}
