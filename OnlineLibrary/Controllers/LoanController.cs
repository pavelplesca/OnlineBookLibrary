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
            if (book != null)
            {
                context.Loans.Add(
                    new Loan
                    {
                        BorrowDate = DateTime.Now,
                        DueDate = DateTime.Now.AddDays(7),
                        ReturnedDate = null, 
                        Status = Status.NowRenting,
                        Book = book,
                        BookID = book.Id                      
                    });

                context.SaveChanges();
            }

            return RedirectToAction("DisplayLoans");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            context.Dispose();
        }
    }
}