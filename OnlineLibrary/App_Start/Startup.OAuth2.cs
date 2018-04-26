using System;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Google;
using System.Security.Claims;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Util.Store;
using Google.Apis.Auth.OAuth2;

namespace OnlineLibrary.App_Start
{
    public class Startup
	{
        private IDataStore dataStore = new FileDataStore(GoogleWebAuthorizationBroker.Folder);

        public void ConfigurationAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(DefaultAuthenticationTypes.ApplicationCookie);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                AuthenticationMode = AuthenticationMode.Passive,
                CookieName = CookieAuthenticationDefaults.CookiePrefix,
                ExpireTimeSpan = TimeSpan.FromMinutes(5),
                LoginPath = new PathString("/User/Login")
            });

            var google = new GoogleOAuth2AuthenticationOptions()
            {
                AuthenticationType = "Google",
                AccessType = "Offline",
                ClientId = MyClientSecrets.ClientID,
                ClientSecret = MyClientSecrets.ClientSecret,
                Provider = new GoogleOAuth2AuthenticationProvider()
                {
					OnAuthenticated = async context =>
                    {
                        var userID = context.Id;
                        context.Identity.AddClaim(new Claim(MyClaimsTypes.GoogleUserID, userID));

                        var tokenResponse = new TokenResponse()
                        {
                            AccessToken = context.AccessToken,
                            RefreshToken = context.RefreshToken,
                            ExpiresInSeconds = (long)context.ExpiresIn.Value.TotalSeconds,
                            Issued = DateTime.Now
                        };

                        await dataStore.StoreAsync(userID, tokenResponse);
                    },
                },
            };

            foreach (var scope in MyRequestedScopes.Scopes)
            {
                google.Scope.Add(scope);
            }

            app.UseGoogleAuthentication(google);
        }
	}
}