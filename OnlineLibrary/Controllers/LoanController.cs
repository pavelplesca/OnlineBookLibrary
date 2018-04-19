using OnlineLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

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
            var result = context.Loans.Include(x => x.Book);
            return View(result);
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
                        BookID = book.Id                      
                    });

                context.SaveChanges();
            }

            return RedirectToAction("DisplayLoans");
        }

        public ActionResult CancelLoan(Book book)
        {
            Loan canceledLoan = context.Loans.Where(x => x.BookID == book.Id).SingleOrDefault();
            context.Loans.Remove(canceledLoan);

            context.SaveChanges();

            return RedirectToAction("DisplayLoans"); ;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            context.Dispose();
        }
    }
}