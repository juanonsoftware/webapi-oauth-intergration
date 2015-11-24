using Rabbit.WebApi;
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
        public dynamic GetCurrentAccount()
        {
            return _accountManagementService.GetAccount((ClaimsIdentity)User.Identity);
        }

        [HttpPost]
        public dynamic CreateAccount([ParameterRequired] CreateAccountViewModel account)
        {
            var accountInfo = new AccountDto()
            {
                Email = account.Email,
                Name = account.Name,
                CustomizationEnabled = account.CustomizationEnabled
            };

            _accountManagementService.CreateOrUpdateAccount((ClaimsIdentity)User.Identity, accountInfo);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
