﻿using OnlineBookLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace OnlineBookLibrary.Controllers
{
    public class BookController : Controller
    {
        
        public ActionResult Index()
        {
            ViewBag.Page = 1;   
            return View();
        }

        private ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult BookDetails(int? id)
        {
            var book = _db.Books.Include(x => x.Tags).ToList().Where(x => x.Id == id).FirstOrDefault();
            if (book == null) return RedirectToAction("Index", "Home");

            return View(book);
        }

        [ChildActionOnly]
        public ActionResult TopLoans()
        {
            IEnumerable<Book> loans = _db.Books.ToList().Take(3);

            return PartialView("_TopLoans", loans);
        }

        public ActionResult TaggedBookPage(int page, string tag)
        {
            var books = _db.Books.Where(b => b.Tags.Select(t => t.Name).Contains(tag));

            if (tag is null || tag == "All")
            {
                books = _db.Books;
            }
            int maxPage = (books.Count() / 6) + 1;
            ViewBag.maxpage = maxPage;

            return PartialView("_BookPage", books.ToList().Skip((page - 1) * 6).Take(6));
        }

        [ChildActionOnly]
        public ActionResult ReturnTags()
        {
            var tags = new List<Tag>() { new Tag() { Id = 0, Name = "All" } };
            tags.AddRange(_db.Tags.ToList());

            return PartialView("_TagDropdown", tags);
        }

        [ChildActionOnly]
        public ActionResult DisplayButtons(string userId, Book book)
        {
            ApplicationUser user = _db.Users.Where(x => x.Id == userId).SingleOrDefault();

            if (!user.IsBanned)
            {
                return PartialView("_UserNotBannedPartial", book);
            }
            return new EmptyResult();
        }

        protected override void Dispose(bool disposing)
        {
            if (_db != null)
            {
                _db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}