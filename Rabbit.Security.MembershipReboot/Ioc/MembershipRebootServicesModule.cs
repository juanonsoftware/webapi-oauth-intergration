using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.Ef;
using BrockAllen.MembershipReboot.WebHost;
using Rabbit.IOC;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace Rabbit.Security.MembershipReboot.Ioc
{
    public class MembershipRebootServicesModule : ModuleBase, IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register(() => new DefaultMembershipRebootDatabase());
            container.Register<IUserAccountRepository, DefaultUserAccountRepository>();
            container.Register(() => new UserAccountService(container.GetInstance<IUserAccountRepository>()));
            container.Register<AuthenticationService>(() => new SamAuthenticationService(container.GetInstance<UserAccountService>()), Lifestyle.Scoped);
        }
    }
}
