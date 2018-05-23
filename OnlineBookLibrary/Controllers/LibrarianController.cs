using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public partial class LibrarianController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Tags()
        {
            var _db = new OnlineLibraryDbContext();
            var tags = _db.Tags;

            return PartialView("_TagList", tags);
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

        [HttpPost]
        public ActionResult AddLibrarian()
        {
            var context = new OnlineLibraryDbContext();
            var userManager = new UserManager<User>(new UserStore<User>(context));

            string name = Request.Form["librarianEmail"];
            string email = Request.Form["librarianEmail"];
            string role = "librarian";
            string password = Request.Form["librarianPassword"];

            var user = new User();

            if (name != "")
            {
                user.UserName = name;
                user.Email = email;


                if (!userManager.Users.Any(u => u.UserName == name))
                {
                    var adminresult = userManager.Create(user, password);

                    if (adminresult.Succeeded)
                    {
                        var result = userManager.AddToRole(user.Id, role);
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", @"Email already exists!");
                }
            }
            else
            {
                ModelState.AddModelError("Email", @"Email adn password are required!");
            }

            return View("Index");
        }
    }
}