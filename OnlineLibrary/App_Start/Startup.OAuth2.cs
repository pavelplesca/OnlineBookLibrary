using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;

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


            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
				AuthenticationType = "Google",
				
            });

        }
	}
}