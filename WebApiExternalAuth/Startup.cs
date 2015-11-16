using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SimpleInjector.Integration.WebApi;
using WebApiExternalAuth;
using WebApiExternalAuth.Configuration;

[assembly: OwinStartup(typeof(Startup))]

namespace WebApiExternalAuth
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var container = IocConfig.BuildContainer();
            var configuration = WebApiConfig.Register();

            // Register dependency resolver
            configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            // Configure OAuth
            appBuilder.ConfigureAuthentication(container.GetInstance<OAuthBearerAuthenticationOptions>());
            appBuilder.ConfigureExternalProviders();
            
            appBuilder.UseCors(CorsOptions.AllowAll);
            appBuilder.UseWebApi(configuration);

            MembershipRebootConfig.Configure();
        }
    }
}