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
        [Authorize]
        public dynamic Get()
        {
            return new
                {
                    Data = DateTime.Now,
                    IsAuthenticated = User.Identity.IsAuthenticated
                };
        }
    }
}