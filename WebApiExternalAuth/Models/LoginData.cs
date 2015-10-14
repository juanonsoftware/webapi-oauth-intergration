using System;

namespace WebApiExternalAuth.Models
{
    public class LoginData
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Profile { get; set; }
        public string ExternalAccessToken { get; set; }
        public TimeSpan ExpiresIn { get; set; }
    }
}
