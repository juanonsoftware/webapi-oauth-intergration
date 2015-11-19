using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using WebApiExternalAuth.Services;

namespace WebApiExternalAuth.Controllers
{
    [Authorize]
    public class AccountController : ApiController
    {
        private readonly IAccountManagementService _accountManagementService;

        public AccountController(IAccountManagementService accountManagementService)
        {
            _accountManagementService = accountManagementService;
        }

        /// <summary>
        /// Get account information
        /// </summary>
        /// <returns></returns>
        public dynamic Get()
        {
            return _accountManagementService.GetAccount((ClaimsIdentity)User.Identity);
        }

        [HttpPost]
        public dynamic CreateOrUpdate(AccountDto accountInfo)
        {
            _accountManagementService.CreateOrUpdateAccount((ClaimsIdentity)User.Identity, accountInfo);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
