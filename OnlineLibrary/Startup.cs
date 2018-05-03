﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using OnlineLibrary.Models;
using Owin;

[assembly: OwinStartup(typeof(OnlineLibrary.Startup))]

namespace OnlineLibrary
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            app.CreatePerOwinContext<OnlineLibraryDb>(OnlineLibraryDb.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/User/Login")
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            app.UseGoogleAuthentication(
                new GoogleOAuth2AuthenticationOptions
                {
                    
                   // AuthenticationType = "Google",
                    ClientId = "107315705740-2fm0vk47q13o96k58ub4rfrd9mkl6cq0.apps.googleusercontent.com",
                    ClientSecret = "QuY47VxJEgGjaw05Ef6-CDJH",
                    Caption = "Google",
                   /* CallbackPath = new PathString("/User/ExternalLoginCallback"),
                    AuthenticationMode = AuthenticationMode.Passive,
                    SignInAsAuthenticationType = app.GetDefaultSignInAsAuthenticationType(),
                    BackchannelTimeout = TimeSpan.FromSeconds(60),
                    BackchannelHttpHandler = new System.Net.Http.WebRequestHandler(),
                    BackchannelCertificateValidator = null,
                    Provider = new GoogleOAuth2AuthenticationProvider()*/
                }
            );

            app.UseFacebookAuthentication(
                appId: "221926618564798",
                appSecret: "c3adfb92c6feaac2cad7b513d0992143");
        }
    }
}
