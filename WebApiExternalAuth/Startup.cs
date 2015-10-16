using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using WebApiExternalAuth;
using WebApiExternalAuth.Configuration;

[assembly: OwinStartup(typeof(Startup))]

namespace WebApiExternalAuth
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var configuration = WebApiConfig.Register();

            appBuilder.UseCors(CorsOptions.AllowAll);

            appBuilder.ConfigureSecurity();

            appBuilder.UseWebApi(configuration);
        }
    }
}