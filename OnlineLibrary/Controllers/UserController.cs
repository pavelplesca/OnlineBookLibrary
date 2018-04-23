using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using OnlineLibrary.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

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

        public ActionResult Index()
        {
            return View();
        }

        [Route("User/Login")]
        public ActionResult Login()
        {
            return View("Authentication");
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
            return RedirectToAction("Index", "User");
        }
    }
}