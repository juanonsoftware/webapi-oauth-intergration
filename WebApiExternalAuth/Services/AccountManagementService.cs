using BrockAllen.MembershipReboot;
using Rabbit.Security;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;

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
            var customizationModes = CustomizationModes.None;

            var userAccount = _userAccountService.GetByEmail(loginData.Email);
            if (userAccount == null)
            {
                var claims = new List<Claim>
                {
                    new Claim(CustomClaims.CustomizationModes, ((int)customizationModes).ToString(CultureInfo.InvariantCulture))
                };

                userAccount = _userAccountService.CreateAccount(accountInfo.Email, Guid.NewGuid().ToString(), accountInfo.Email, claims: claims);
            }
            else
            {
                var modesString = userAccount.Claims.First(x => x.Type == CustomClaims.CustomizationModes).Value;
                customizationModes = BuildModes(modesString, accountInfo);

                var claims = new List<Claim>
                {
                    new Claim(CustomClaims.CustomizationModes, ((int)customizationModes).ToString(CultureInfo.InvariantCulture))
                };
            }

            _userAccountService.AddOrUpdateLinkedAccount(userAccount, loginData.ProviderName, loginData.ProviderKey, claimsIdentity.Claims);
        }

        public AccountLightDto GetAccount(ClaimsIdentity claimsIdentity)
        {
            var loginData = _loginDataParser.Parse(claimsIdentity);

            return new AccountLightDto()
            {
                Email = loginData.Email,
                Name = loginData.Name,
                ProviderName = loginData.ProviderName,
                Profile = loginData.Profile
            };
        }

        private CustomizationModes BuildModes(string currentModes, AccountDto accountInfo)
        {
            var customizationModes = (CustomizationModes)int.Parse(currentModes);

            if (accountInfo.CustomizationEnabled)
            {
                customizationModes |= CustomizationModes.General;
            }
            else
            {
                customizationModes &= ~CustomizationModes.General;
            }

            //if (accountInfo.TwoFactorsAuthenticationEnabled)
            //{
            //    customizationModes |= CustomizationModes.TwoFactorsAuthentication;
            //}
            //else
            //{
            //    customizationModes &= ~CustomizationModes.TwoFactorsAuthentication;
            //}

            return customizationModes;
        }
    }
}
