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
            
            return View(Book);
        }
    }
}