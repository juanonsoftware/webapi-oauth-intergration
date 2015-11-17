using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
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
                    ConsumerKey = "dj0yJmk9Y2Y3a3Z4YTFhQ1k2JmQ9WVdrOU5saExSazR6TmpJbWNHbzlNQS0tJnM9Y29uc3VtZXJzZWNyZXQmeD1jNA--",
                    ConsumerSecret = "fba4504c1d0fe651e3a626248447edc7fad6e93f",
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
                ConsumerKey = "YSsU1AO20L8Yn0PJNpEj78LWK",
                ConsumerSecret = "vqqJOIo8CV8Nd8MIqBPdPS0UgArJv9yvf4xe72qvrl0PZ4qmaZ",
                Provider = new CustomTwitterAuthenticationProvider(),
                BackchannelCertificateValidator = new CertificateSubjectKeyIdentifierValidator(new string[]
                {
                    "A5EF0B11CEC04103A34A659048B21CE0572D7D47", // VeriSign Class 3 Secure Server CA - G2
                    "0D445C165344C1827E1D20AB25F40163D8BE79A5", // VeriSign Class 3 Secure Server CA - G3
                    "7FD365A7C2DDECBBF03009F34339FA02AF333133", // VeriSign Class 3 Public Primary Certification Authority - G5
                    "39A55D933676616E73A761DFA16A7E59CDE66FAD", // Symantec Class 3 Secure Server CA - G4
                    "4eb6d578499b1ccf5f581ead56be3d9b6744a5e5", // VeriSign Class 3 Primary CA - G5
                    "5168FF90AF0207753CCCD9656462A212B859723B", // DigiCert SHA2 High Assurance Server C‎A 
                    "B13EC36903F8BF4701D498261A0802EF63642BC3" // DigiCert High Assurance EV Root CA
                })
            });

            app.UseLinkedInAuthentication(new LinkedInAuthenticationOptions()
            {
                ClientId = "77m16velrufp9j",
                ClientSecret = "kuNnueDpkVlj2nz4",
                Provider = new CustomLinkedInAuthenticationProvider(),
            });
        }
    }
}
