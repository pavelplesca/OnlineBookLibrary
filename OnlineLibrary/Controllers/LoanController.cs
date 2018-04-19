using OnlineLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineLibrary.Controllers
{
    public class LoanController : Controller
    {
        private OnlineLibraryDb context = new OnlineLibraryDb();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DisplayLoans()
        {          
            return View();
        }

        public ActionResult CreateLoan(Book book)
        {           
            return RedirectToAction("DisplayLoans");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            context.Dispose();
        }
    }
}