using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace OnlineLibrary.Models
{
    public class ApplicationUserManager:UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> store) : base(store)
        {
            UserValidator = new UserValidator<User>(this) { AllowOnlyAlphanumericUserNames = false};
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            OnlineLibraryDb db = context.Get<OnlineLibraryDb>();
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<User>(db));
            return manager;
        }

        
    }
}