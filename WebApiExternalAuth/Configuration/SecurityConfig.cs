using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.MicrosoftAccount;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Twitter;
using Owin;
using Owin.Security.Providers.GitHub;
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
                Provider = new GitHubCustomAuthenticationProvider()
            });

            app.UseTwitterAuthentication(new TwitterAuthenticationOptions()
            {
                ConsumerKey = "ooLB5tn3zkIyfI9CID3DmiTld",
                ConsumerSecret = "K5XsAmByQoW8oGIXjE1wIqSQUhx5Eztnzi5aUHdRLdGcYfGEJi",
                BackchannelCertificateValidator = new CertificateSubjectKeyIdentifierValidator(new[]
                {
                    "A5EF0B11CEC04103A34A659048B21CE0572D7D47", // VeriSign Class 3 Secure Server CA - G2
                    "0D445C165344C1827E1D20AB25F40163D8BE79A5", // VeriSign Class 3 Secure Server CA - G3
                    "7FD365A7C2DDECBBF03009F34339FA02AF333133", // VeriSign Class 3 Public Primary Certification Authority - G5
                    "39A55D933676616E73A761DFA16A7E59CDE66FAD", // Symantec Class 3 Secure Server CA - G4
                    "4eb6d578499b1ccf5f581ead56be3d9b6744a5e5", // VeriSign Class 3 Primary CA - G5
                    "5168FF90AF0207753CCCD9656462A212B859723B", // DigiCert SHA2 High Assurance Server C‎A 
                    "B13EC36903F8BF4701D498261A0802EF63642BC3", // DigiCert High Assurance EV Root CA
                }),
                Provider = new TwitterCustomAuthenticationProvider()
            });
        }
    }
}
