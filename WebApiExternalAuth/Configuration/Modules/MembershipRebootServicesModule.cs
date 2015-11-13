using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.Ef;
using Rabbit.IOC;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace WebApiExternalAuth.Configuration.Modules
{
    public class MembershipRebootServicesModule : ModuleBase, IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register(() => new DefaultMembershipRebootDatabase());
            container.Register<IUserAccountRepository, DefaultUserAccountRepository>();
            container.Register(() => new UserAccountService(container.GetInstance<IUserAccountRepository>()), Lifestyle.Scoped);
        }
    }
}
