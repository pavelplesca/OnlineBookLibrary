using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OnlineBookLibrary.Models;
using OnlineBookLibrary.Persistence;

namespace OnlineBookLibrary.Controllers
{
    [Authorize(Roles = "librarian")]
    public class LibrarianController : Controller
    {
        
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Tags()
        {
            var _db = new OnlineLibraryDbContext();
            var tags = _db.Tags;

            return PartialView("_TagList",tags);
        }


        public ActionResult Users()
        {
            var _db = new OnlineLibraryDbContext();
            var users = _db.Users.ToList();

            return PartialView("_UserList", users);
        }

        public ActionResult Books()
        {
            var _db = new OnlineLibraryDbContext();
            var books = _db.Books;

            return PartialView("_BookList", books);
        }
    }
}