using BrockAllen.MembershipReboot.Ef;

namespace WebApiExternalAuth.Models
{
    public class CustomDbContext : MembershipRebootDbContext<CustomUserAccount>
    {
    }
}