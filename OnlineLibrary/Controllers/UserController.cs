using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineLibrary.Controllers
{
    public class UserController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        [Route("User/Login")]
        public ActionResult Login()
        {
            return View("Authentication");
        }
    }
}