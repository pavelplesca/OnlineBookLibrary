using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using OnlineLibrary.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace OnlineLibrary.Controllers
{
    public class UserController : Controller
    {
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Index()
        {
            return Content("Placeholder");
        }

        [HttpGet]
        [Route("User/Login")]
        public ActionResult Login()
        {
            //ViewBag.returnUrl = returnUrl;
            return View("Authentication");
        }

        [HttpPost]
        [Route("User/Login")]
        public ActionResult Login(UserAuthModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = UserManager.Find(model.Email, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Wrong email or password.");
                }
                else
                {
                    ClaimsIdentity claim =  UserManager.CreateIdentity(user,
                        DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    if (String.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");
                    return Redirect(returnUrl);
                }
            }
            
            return View("Authentication",model);
        }


        //public async Task<ActionResult> Login(UserAuthModel model, string returnUrl)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        User user =  UserManager.Find(model.Email, model.Password);
        //        if (user == null)
        //        {
        //            ModelState.AddModelError("", "Wrong email or password.");
        //        }
        //        else
        //        {
        //            ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
        //                DefaultAuthenticationTypes.ApplicationCookie);
        //            AuthenticationManager.SignOut();
        //            AuthenticationManager.SignIn(new AuthenticationProperties
        //            {
        //                IsPersistent = true
        //            }, claim);
        //            if (String.IsNullOrEmpty(returnUrl))
        //                return RedirectToAction("Index", "Home");
        //            return Redirect(returnUrl);
        //        }
        //    }
        //    ViewBag.returnUrl = returnUrl;
        //    return View(model);
        //}
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }



        [HttpGet]
        public ActionResult Register()
        {
            return RedirectToAction("Login", "User");
        }

        [HttpPost]
        public ActionResult Register(UserAuthModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { UserName = model.Email, Email = model.Email};
                IdentityResult result =  UserManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "User");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View("Authentication", model);
        }
    }
}