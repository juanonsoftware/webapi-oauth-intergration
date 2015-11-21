using System.Data.Entity;
using BrockAllen.MembershipReboot.Ef;

namespace WebApiExternalAuth.Configuration
{
    public static class MembershipRebootConfig
    {
        public static void Configure()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DefaultMembershipRebootDatabase, BrockAllen.MembershipReboot.Ef.Migrations.Configuration>());


        }
    }
}