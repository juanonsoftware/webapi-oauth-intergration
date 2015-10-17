using Rabbit.IOC;
using Rabbit.Security;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace WebApiExternalAuth.Configuration.Modules
{
    public class GlobalServicesModule : ModuleBase, IPackage
    {
        public void RegisterServices(Container container)
        {
            container.RegisterPerWebRequest<ILoginDataParser, OAuthLoginDataParser>();
        }
    }
}