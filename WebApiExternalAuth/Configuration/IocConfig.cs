using Rabbit.IOC;
using Rabbit.Security.MembershipReboot;
using Rabbit.SimpleInjectorDemo.IocModules;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Packaging;
using System.Linq;
using System.Web.Http;

namespace WebApiExternalAuth.Configuration
{
    public static class IocConfig
    {
        public static void RegisterDependencyResolver(this HttpConfiguration configuration)
        {
            var container = BuildContainer();
            configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        public static Container BuildContainer()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            ModuleHelper.GetModuleTypes(typeof(WebApiConfig).Assembly,
                typeof(DemoServicesModule).Assembly,
                typeof(MembershipRebootConfig).Assembly)
                .CreateModules()
                .Cast<IPackage>()
                .ToList()
                .ForEach(x => x.RegisterServices(container));

            return container;
        }
    }
}