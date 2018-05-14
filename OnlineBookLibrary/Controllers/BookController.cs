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
        Logger logger = new Logger("BookController");
        private readonly BookRepository _bookRepository = new BookRepository();

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
        
        public ActionResult BookDetails(int? id)
        {
            logger.Log("BookDetails started");
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
            IEnumerable<Book> books = _bookRepository.GetTopLoanedBooks();
            logger.Log("TopLoans  return PartialView");
            return PartialView("_TopLoans", books);
        }

        public ActionResult TaggedBookPage(int page, string tag)
        {
            logger.Log("TaggedBookPage started");
            if (tag == null)
                tag = "All";
            var books = _bookRepository.GetBooksByTag(tag);
            books = _bookRepository.GetPageOfBooks(books, page);
            int maxPage = (books.Count() / 6) + 1;
            ViewBag.maxpage = maxPage;
            logger.Log("TaggedBookPage return PartialView(_BookPage)");

            return PartialView("_BookPage", books);
        }

        [ChildActionOnly]
        public ActionResult ReturnTags()
        {
            logger.Log("ReturnTags started");
            var tags = _bookRepository.GetTags();
            logger.Log("ReturnTags PartialView (_TagDropdown)");
            return PartialView("_TagDropdown", tags);
        }

        [ChildActionOnly]
        public ActionResult DisplayButtons(string userId, Book book)
        {
            logger.Log("DisplayButtons started");
            var um = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            
            User user = um.FindById(userId); 

            if (!user.IsBanned)
            {
                logger.Log("DisplayButtons PartialView(_UserNotBannedPartial)");
                return PartialView("_UserNotBannedPartial", book);
            }
            return new EmptyResult();
        }

        protected override void Dispose(bool disposing)
        {
            if (_bookRepository != null)
            {
                _bookRepository.Dispose();
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

