using Microsoft.Owin.Security.OAuth;
using Rabbit.IOC;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace WebApiExternalAuth.Configuration.Modules
{
    public class OAuthModule : ModuleBase, IPackage
    {
        public void RegisterServices(Container container)
        {
            container.RegisterSingleton<OAuthBearerAuthenticationOptions>();
        }
    }
}