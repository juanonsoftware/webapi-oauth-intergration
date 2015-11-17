using Rabbit.IOC;
using Rabbit.Security;
using SimpleInjector;
using SimpleInjector.Packaging;
using WebApiExternalAuth.Models;

namespace WebApiExternalAuth.Configuration.Modules
{
    public class GlobalServicesModule : ModuleBase, IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IExternalLoginDataParserFactory, ExternalLoginDataParserFactory>();
            container.Register<ILoginDataParser, OAuthLoginDataParser>(Lifestyle.Scoped);
        }
    }
}