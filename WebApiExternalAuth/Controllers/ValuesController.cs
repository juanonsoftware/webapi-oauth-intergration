using Rabbit.SimpleInjectorDemo.Services;
using System;
using System.Web.Http;

namespace WebApiExternalAuth.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly IListingService _listingService;

        public ValuesController(IListingService listingService)
        {
            _listingService = listingService;
        }

        // GET api/values
        public dynamic Get()
        {
            return new
                {
                    Data = DateTime.Now,
                    User.Identity.IsAuthenticated,
                    User.Identity.Name,
                    User.Identity.AuthenticationType
                };
        }
    }
}