namespace WebApiExternalAuth.Services
{
    public class AccountLightDto
    {
        public string Email { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Provider name that user authenticated with (Google, Yahoo, Twitter...)
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// The link to external account
        /// </summary>
        public string Profile { get; set; }
    }
}