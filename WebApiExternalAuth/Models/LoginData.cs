using System.Collections.Generic;

namespace WebApiExternalAuth.Models
{
    public class LoginData
    {
        public LoginData()
        {
            Properties = new Dictionary<string, object>();
        }

        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Profile { get; set; }
        public string ExternalAccessToken { get; set; }
        public IDictionary<string, object> Properties { get; private set; }
    }
}
