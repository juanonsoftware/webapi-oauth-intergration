using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.MicrosoftAccount;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Twitter;
using Owin;
using Owin.Security.Providers.GitHub;
using Owin.Security.Providers.LinkedIn;
using Owin.Security.Providers.Yahoo;
using WebApiExternalAuth.Providers;

namespace WebApiExternalAuth.Configuration
{
    public static class SecurityConfig
    {
        public static void ConfigureAuthentication(this IAppBuilder app, OAuthBearerAuthenticationOptions options)
        {
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseOAuthBearerAuthentication(options);
        }

        public static void ConfigureExternalProviders(this IAppBuilder app)
        {
            // Configure google authentication
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
                {
                    ClientId = "592811454485-lbltg7v9gibiets5pekaasrf0hiltjqg.apps.googleusercontent.com",
                    ClientSecret = "nr9MSTaYquDnojiMd8GsjE2m",
                    Provider = new CustomGoogleAuthenticationProvider(),
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
                    ClientSecret = "K1BYOzio7sP36np84MDNdd60jXEvQSH-",
                    Provider = new MicrosoftAccountCustomAuthenticationProvider()
                });

            app.UseYahooAuthentication(new YahooAuthenticationOptions()
                {
                    ConsumerKey = "dj0yJmk9YUxMbXZUY05CWXNaJmQ9WVdrOVIwOVZRalpJTkhNbWNHbzlNQS0tJnM9Y29uc3VtZXJzZWNyZXQmeD05Mg--",
                    ConsumerSecret = "e398b55be76c5646103c26cc8a1d6a47d26b64dd",
                    Provider = new YahooCustomAuthenticationProvider()
                });

            app.UseGitHubAuthentication(new GitHubAuthenticationOptions()
            {
                ClientId = "78e903a27192ee724f5b",
                ClientSecret = "09aaed1ef94fda54c1430bf8b58f51b8e94733d9",
                Provider = new CustomGitHubAuthenticationProvider()
            });

            app.UseTwitterAuthentication(new TwitterAuthenticationOptions()
            {
                ConsumerKey = "Aoz2iCQNYLNuoXWqPaZK0p7bY",
                ConsumerSecret = "whC2WcbvEXUTDV3azJzOfDM0JCAnawFcV0fVK1nwqxW20HzIsT",
                Provider = new CustomTwitterAuthenticationProvider()
            });

            app.UseLinkedInAuthentication(new LinkedInAuthenticationOptions()
            {
                ClientId = "77m16velrufp9j",
                ClientSecret = "kuNnueDpkVlj2nz4",
                Provider = new CustomLinkedInAuthenticationProvider()
            });
        }
    }
}
