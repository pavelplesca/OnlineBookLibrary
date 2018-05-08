using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Web.Security;
using OnlineBookLibrary.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using OnlineBookLibrary.Persistence;

namespace OnlineBookLibrary.Controllers
{
    public class UserController : Controller
    {
        private readonly OnlineLibraryDbContext _db = new OnlineLibraryDbContext();

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

        private RoleManager<IdentityRole> RoleManager
        {
            get
            {
                return new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));
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

        [HttpGet]
        [Route("User/Authentication")]
        public ActionResult Authentication()
        {
            //ViewBag.returnUrl = returnUrl;
            return Login();
        }

        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("ExternalLoginCallback", "User", new { returnUrl = returnUrl })
            };
            HttpContext.GetOwinContext().Authentication.Challenge(properties, provider);
            return new HttpUnauthorizedResult();
        }

        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            ExternalLoginInfo loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            //ExternalLoginInfo loginInfo =  AuthenticationManager.GetExternalLoginInfo();
            if (loginInfo == null)
            {
                //return RedirectToAction("Login");
                return Content("Error: loginInfo is null");
            }

            User user = await UserManager.FindByEmailAsync(loginInfo.Email);

            if (user == null)
            {
                user = new User { Email = loginInfo.Email, UserName = loginInfo.Email };

                IdentityResult result = await UserManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    return Content(string.Join(", ", result.Errors));
                }
                else
                {
                    result = await UserManager.AddLoginAsync(user.Id, loginInfo.Login);
                    if (!result.Succeeded)
                    {
                        return Content(string.Join(", ", result.Errors));
                    }
                }
            }
            ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user,
                DefaultAuthenticationTypes.ApplicationCookie);
            ident.AddClaims(loginInfo.ExternalIdentity.Claims);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = false }, ident);
            return Redirect(returnUrl ?? "/");
        }

        [HttpPost]
        [Route("User/Login")]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserAuthModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = UserManager.Find(model.UserLoginModel.Email, model.UserLoginModel.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Wrong email or password.");
                }
                else
                {
                    ClaimsIdentity claim = UserManager.CreateIdentity(user,
                        DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claim);
                    if (String.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");
                    return Redirect(returnUrl);
                }
            }
            return View("Authentication", model);
        }

        // [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            //Session.Remove("facebooktoken");
            FormsAuthentication.SignOut();
            
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return RedirectToAction("Login", "User");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserAuthModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { UserName = model.UserRegisterModel.Email, Email = model.UserRegisterModel.Email };
                IdentityResult result = UserManager.Create(user, model.UserRegisterModel.Password);
                if (result.Succeeded)
                {
                    model.UserLoginModel = new UserLoginModel() { Email = model.UserRegisterModel.Email, Password = model.UserRegisterModel.Password };
                    ClaimsIdentity claim = UserManager.CreateIdentity(user,
                        DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
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

        protected override void Dispose(bool disposing)
        {
            if (_db != null)
                _db.Dispose();
            base.Dispose(disposing);
        }
    }
}