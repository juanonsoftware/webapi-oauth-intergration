using System;
using System.Web.Http;

namespace WebApiExternalAuth.Controllers
{
    public static class ControllerExtensions
    {
        public static string GetControllerName(this ApiController controller)
        {
            var typeName = controller.GetType().Name;
            return typeName.Substring(0, typeName.IndexOf("Controller", StringComparison.InvariantCulture));
        }
    }
}