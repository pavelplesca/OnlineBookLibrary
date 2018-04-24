using OnlineLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineLibrary.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            ViewBag.Page = 1;
            return View();
        }


        private OnlineLibraryDb _db = new OnlineLibraryDb();

        public ActionResult BookDetails(int? id)
        {
            var book = _db.Books.ToList().Where(x=> x.Id == id).FirstOrDefault();
            if (book == null) return RedirectToAction("Index", "Home");
            return View(book);
        }

        [ChildActionOnly]
        public ActionResult TopLoans()
        {
            IEnumerable<Book> loans = _db.Books.ToList().Take(3);

            return PartialView("_TopLoans", loans);
        }


        [ChildActionOnly]
        public ActionResult TaggedBookPage(int page, string tag)
        {
            var books =_db.Books.Where(b => b.Tags.Select(t => t.Name).Contains(tag));
            int maxPage = (books.Count() / 6)+1;
            ViewBag.maxpage = maxPage;

            if (tag is null || tag == "All")
            {
                books = _db.Books;
            }
            return PartialView("_BookPage", books.ToList().Skip((page - 1) * 6).Take(6));
        }

        [ChildActionOnly]
        public ActionResult ReturnTags()
        {
            var tags = new List<Tag>(){new Tag(){Id = 0, Name = "All"}};
            tags.AddRange(_db.Tags.ToList()); 
            return PartialView("_TagDropdown", tags);
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
