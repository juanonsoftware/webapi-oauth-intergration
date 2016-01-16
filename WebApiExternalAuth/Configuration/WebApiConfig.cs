using Newtonsoft.Json;
using Rabbit.WebApi.Filters;
using Rabbit.WebApi.Filters.ValidationHandlers;
using System.Web.Http;

namespace WebApiExternalAuth.Configuration
{
    public static class WebApiConfig
    {
        public static HttpConfiguration Register()
        {
            var config = new HttpConfiguration();

            config.RegisterRoutes();
            config.OptimizeFormatters();
            config.RegisterFilters();

            return config;
        }

        private static void RegisterRoutes(this HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "ApiAction",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void OptimizeFormatters(this HttpConfiguration config)
        {
            // Remove XML serialization
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Update Json serialization
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
        }

        private static void RegisterFilters(this HttpConfiguration config)
        {
            config.Filters.Add(new ModelValidationFilterAttribute(new ParameterRequiredValidationHandler()));
            config.Filters.Add(new ModelValidationFilterAttribute(new BadRequestValidationHandler()));
        }
    }
}
