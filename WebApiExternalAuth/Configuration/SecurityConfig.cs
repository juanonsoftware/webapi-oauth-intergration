using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.MicrosoftAccount;
using Owin;
using WebApiExternalAuth.Models;

namespace WebApiExternalAuth.Configuration
{
    public static class SecurityConfig
    {
        public static void ConfigureSecurity(this IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ExternalCookie
            });

            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure google authentication
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
                {
                    ClientId = "592811454485-lbltg7v9gibiets5pekaasrf0hiltjqg.apps.googleusercontent.com",
                    ClientSecret = "nr9MSTaYquDnojiMd8GsjE2m",
                    Provider = new GoogleCustomAuthenticationProvider(),
                });

            app.UseFacebookAuthentication(new FacebookAuthenticationOptions()
                {
                    AppId = "118560908500179",
                    AppSecret = "a0650638ae246afa9d974e3a3df45deb",
                    Provider = new FacebookCustomAuthenticationProvider()
                });

            app.UseMicrosoftAccountAuthentication(new MicrosoftAccountAuthenticationOptions()
                {
                    ClientId = "0000000044167B8E",
                    ClientSecret = "K1BYOzio7sP36np84MDNdd60jXEvQSH-"
                });
        }
    }
}