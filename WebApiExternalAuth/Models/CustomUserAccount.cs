using BrockAllen.MembershipReboot.Relational;

namespace WebApiExternalAuth.Models
{
    public class CustomUserAccount : RelationalUserAccount
    {
        public string DisplayName { get; set; }
    }
}