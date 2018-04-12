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
            return View();
        }


        private OnlineLibraryDb _db = new OnlineLibraryDb();

        public ActionResult BookDetails(int? id)
        {
            
            var Book = _db.Books.ToList().Where(x=> x.Id == id).FirstOrDefault();
            if (Book == null) return RedirectToAction("Index", "Home");
            return View(Book);
        }

        [ChildActionOnly]
        public ActionResult TopLoans()
        {
            IEnumerable<Book> loans = _db.Books.ToList().Take(3);

            return PartialView("_TopLoans", loans);
        }

        [ChildActionOnly]
        public ActionResult BookPage(int page)
        {       
            
            return PartialView("_BookPage", _db.Books.ToList().Skip(page * 6).Take(6));
        }

        [ChildActionOnly]
        public ActionResult getTags()
        {
            var tags = _db.Tags.ToList();
            return PartialView("_TagsView", tags);
        }
    }
}