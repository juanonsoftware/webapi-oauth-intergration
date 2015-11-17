using BrockAllen.MembershipReboot;
using Rabbit.Security;
using System;
using System.Security.Claims;
using WebApiExternalAuth.Models;

namespace WebApiExternalAuth.Services
{
    public class AccountManagementService : IAccountManagementService
    {
        private readonly ILoginDataParser _loginDataParser;
        private readonly UserAccountService _userAccountService;

        public AccountManagementService(ILoginDataParser loginDataParser, AuthenticationService authenticationService)
        {
            _loginDataParser = loginDataParser;
            _userAccountService = authenticationService.UserAccountService;
        }

        public void CreateOrUpdateAccount(ClaimsIdentity claimsIdentity, AccountDto accountInfo)
        {
            var loginData = _loginDataParser.Parse(claimsIdentity);

            var userAccount = _userAccountService.GetByEmail(loginData.Email);
            if (userAccount == null)
            {
                var userName = loginData.GetUserName();
                userAccount = _userAccountService.CreateAccount(userName, Guid.NewGuid().ToString(), loginData.Email);
            }

            _userAccountService.AddOrUpdateLinkedAccount(userAccount, loginData.ProviderName, loginData.ProviderKey, claimsIdentity.Claims);
        }
    }
}