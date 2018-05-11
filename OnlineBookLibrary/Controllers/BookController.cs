using OnlineBookLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Diagnostics;
using System.Web.Security;
using OnlineBookLibrary.Persistence;
using OnlineBookLibrary.Persistence.Repositories;
using OnlineBookLibrary.Helpers;

namespace OnlineBookLibrary.Controllers
{
    public class BookController : Controller
    {
        Logger logger = new Logger("BookController");

        public BookController()
        {
            logger.Log("Book Controller started:");
        }
        
        // GET: Book
        public ActionResult Index()
        {
            logger.Log("Index started");
            ViewBag.Page = 1;
            return View();
        }

        private readonly OnlineLibraryDbContext _db = new OnlineLibraryDbContext();
        private readonly BookRepository _bookRepository = new BookRepository();
        
        public ActionResult BookDetails(int? id)
        {
            logger.Log("BookDetails started");
            //var book = _db.Books.Include(x => x.Tags).ToList().Where(x => x.Id == id).FirstOrDefault();
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var book = _bookRepository.GetBookDetailsById((int)id);
            if (book == null)
            {
                return RedirectToAction("Index", "Home");
            }

            logger.Log("BookDetails return View(book)");
            return View(book);
        }

        [ChildActionOnly]
        public ActionResult TopLoans()
        {
            logger.Log("TopLoans started");
            IEnumerable<Book> loans = _db.Books.ToList().Take(3);
            logger.Log("TopLoans  return PartialView");
            return PartialView("_TopLoans", loans);
        }

        public ActionResult TaggedBookPage(int page, string tag)
        {
            logger.Log("TaggedBookPage started");
            var books = _db.Books.Where(b => b.Tags.Select(t => t.Name).Contains(tag));

            if (tag is null || tag == "All")
            {
                books = _db.Books;
            }
            int maxPage = (books.Count() / 6) + 1;
            ViewBag.maxpage = maxPage;

            logger.Log("TaggedBookPage return PartialView(_BookPage)");
            return PartialView("_BookPage", books.ToList().Skip((page - 1) * 6).Take(6));
        }

        [ChildActionOnly]
        public ActionResult ReturnTags()
        {
            logger.Log("ReturnTags started");
            var tags = new List<Tag>() { new Tag() { Id = 0, Name = "All" } };
            tags.AddRange(_db.Tags.ToList());
            logger.Log("ReturnTags PartialView (_TagDropdown)");
            return PartialView("_TagDropdown", tags);
        }

        [ChildActionOnly]
        public ActionResult DisplayButtons(string userId, Book book)
        {
            logger.Log("DisplayButtons started");
            User user = _db.Users.Where(x => x.Id == userId).SingleOrDefault();

            if (!user.IsBanned)
            {
                logger.Log("DisplayButtons PartialView(_UserNotBannedPartial)");
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

            if (logger != null)
            {
                logger.Log("Controller disposed");
                logger.Dispose();
            }
        }
    }
}
/*
 * ViewBag.Page = page;
            int booksOnPage = 6;
            var books = _db.Books.ToList();

            int maxPage = books.Count/ booksOnPage

            books=(List<Book>)books.Skip((page - 1) * booksOnPage).Take(booksOnPage);
            if (books.Count > 0) return PartialView("_BookPage", books);
            else return Content("No any books");
 *
 */
