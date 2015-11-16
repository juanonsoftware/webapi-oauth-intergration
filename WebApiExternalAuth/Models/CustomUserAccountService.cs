using BrockAllen.MembershipReboot;

namespace WebApiExternalAuth.Models
{
    public class CustomUserAccountService : UserAccountService<CustomUserAccount>
    {
        public CustomUserAccountService(IUserAccountRepository<CustomUserAccount> userRepository)
            : base(userRepository)
        {
        }

        public CustomUserAccountService(MembershipRebootConfiguration<CustomUserAccount> configuration, IUserAccountRepository<CustomUserAccount> userRepository)
            : base(configuration, userRepository)
        {
        }
    }
}