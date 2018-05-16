using OnlineBookLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Diagnostics;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OnlineBookLibrary.Persistence;
using OnlineBookLibrary.Persistence.Repositories;
using OnlineBookLibrary.Helpers;

namespace OnlineBookLibrary.Controllers
{
    public class BookController : Controller
    {
        private IBookRepository _bookRepository;

        public BookController(IBookRepository r)
        {
            _bookRepository = r;
        }
        
        // GET: Book
        public ActionResult Index()
        {
            ViewBag.Page = 1;
            return View();
        }
        
        public ActionResult BookDetails(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var book = _bookRepository.GetBookDetailsById((int)id);
            if (book == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(book);
        }

        [ChildActionOnly]
        public ActionResult TopLoans()
        {
            IEnumerable<Book> books = _bookRepository.GetTopLoanedBooks();
            return PartialView("_TopLoans", books);
        }

        public ActionResult TaggedBookPage(int page, string tag)
        {
            if (tag == null)
                tag = "All";
            var books = _bookRepository.GetBooksByTag(tag);
            books = _bookRepository.GetPageOfBooks(books, page);
            int maxPage = (books.Count() / 6) + 1;
            ViewBag.maxpage = maxPage;
            
            return PartialView("_BookPage", books);
        }

        [ChildActionOnly]
        public ActionResult ReturnTags()
        {
            var tags = _bookRepository.GetTags();
            return PartialView("_TagDropdown", tags);
        }

        [ChildActionOnly]
        public ActionResult DisplayButtons(string userId, Book book)
        {
            var um = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            
            User user = um.FindById(userId); 

            if (!user.IsBanned)
            {
                return PartialView("_UserNotBannedPartial", book);
            }
            return new EmptyResult();
        }

        protected override void Dispose(bool disposing)
        {
            if (_bookRepository != null)
            {
                ((IDisposable)_bookRepository).Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

