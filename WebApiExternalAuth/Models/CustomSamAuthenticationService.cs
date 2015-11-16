using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.WebHost;

namespace WebApiExternalAuth.Models
{
    public class CustomSamAuthenticationService : SamAuthenticationService<CustomUserAccount>
    {
        public CustomSamAuthenticationService(UserAccountService<CustomUserAccount> userService)
            : base(userService)
        {
        }
    }
}