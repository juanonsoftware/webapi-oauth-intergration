using BrockAllen.MembershipReboot.Ef;
using BrockAllen.MembershipReboot.Ef.Migrations;
using System.Data.Entity;

namespace Rabbit.Security.MembershipReboot
{
    public static class MembershipRebootConfig
    {
        public static void Configure()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DefaultMembershipRebootDatabase, Configuration>());
        }
    }
}