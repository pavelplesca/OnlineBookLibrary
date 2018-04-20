using OnlineLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using OnlineLibrary.Persistence.Repositories;

namespace OnlineLibrary.Controllers
{
    public class LoanController : Controller
    {
        private LoanRepository loanRepository;

        public LoanController()
        {
            loanRepository = new LoanRepository();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DisplayLoans()
        {           
            return View();
        }

        public ActionResult CreateLoan(int? bookID, int? userID)
        {
            if(bookID != null && userID != null)
            {
                loanRepository.CreateLoan(bookID.Value, userID.Value);
                loanRepository.SaveAndDispose();

                return RedirectToAction("DisplayLoans");
            }
            return View("_Error");
        }

        public ActionResult CancelLoan(int? bookID, int? userID)
        {
            if (bookID != null && userID != null)
            {
                loanRepository.CancelLoan(bookID.Value, userID.Value);
                loanRepository.SaveAndDispose();

                return RedirectToAction("DisplayLoans");
            }
            return View("_Error");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}