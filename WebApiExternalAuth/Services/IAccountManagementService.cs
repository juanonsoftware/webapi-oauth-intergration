using System.Security.Claims;

namespace WebApiExternalAuth.Services
{
    public interface IAccountManagementService
    {
        void CreateOrUpdateAccount(ClaimsIdentity claimsIdentity, AccountDto accountInfo);
    }
}