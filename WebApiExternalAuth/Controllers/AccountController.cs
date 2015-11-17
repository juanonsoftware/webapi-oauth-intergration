using Microsoft.AspNet.Identity;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using WebApiExternalAuth.Services;

namespace WebApiExternalAuth.Controllers
{
    [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
    public class AccountController : ApiController
    {
        private readonly IAccountManagementService _accountManagementService;

        public AccountController(IAccountManagementService accountManagementService)
        {
            _accountManagementService = accountManagementService;
        }

        [HttpPost]
        public dynamic CreateOrUpdate(AccountDto accountInfo)
        {
            _accountManagementService.CreateOrUpdateAccount((ClaimsIdentity)User.Identity, accountInfo);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}