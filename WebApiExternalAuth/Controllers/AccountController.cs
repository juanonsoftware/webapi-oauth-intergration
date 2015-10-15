using System.Web.Http;

namespace WebApiExternalAuth.Controllers
{
    public class AccountController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Authenticate(string provider, string returnUrl = null)
        {
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = Url.Link("DefaultApi", new { controller = "Values" });
            }

            return new ChallengeResult(provider, returnUrl, Request);
        }
    }
}