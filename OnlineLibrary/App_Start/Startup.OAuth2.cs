using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Microsoft.Owin.Security.Google;

namespace OnlineLibrary.App_Start
{
	public class Startup
	{
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

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
            {
                AuthenticationType = "Google",
                AccessType = "Offline",
                ClientId = "43668616772-l20nog942hvh70tnntro59ujecm9odg9.apps.googleusercontent.com",
                ClientSecret = "mvg3LxDcRCo9s3iJcuonTGUv",
                Provider = new GoogleOAuth2AuthenticationProvider()
            });


			
        }
	}
}