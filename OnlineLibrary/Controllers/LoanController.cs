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

        public ActionResult DisplayLoans(int? userID)
        {       
            return View();
        }

        [ChildActionOnly]
        public ActionResult DisplayHistory(int? userID)
        {
            if (userID.HasValue)
            {
                ICollection<Loan> loanHistory = loanRepository.ReturnLoanHistory(userID.Value).ToList();

                if(loanHistory.Count != 0)
                {
                    return PartialView("LoanHistoryPartial", loanHistory);
                }

                return PartialView("EmptyHistoryPartial");
            }

            return View("_Error");            
        }

        [ChildActionOnly]
        public ActionResult DisplayActiveLoan(int? userID)
        {
            if (userID.HasValue)
            {
                Loan activeLoan = loanRepository.ReturnActiveLoan(userID.Value);

                if (activeLoan != null)
                {
                    return PartialView("ActiveLoanPartial", activeLoan);
                }

                return PartialView("EmptyLoanPartial");
            }

            return View("_Error");
        }

        [ChildActionOnly]
        public ActionResult CreateLoan(int? bookID, int? userID)
        {
            if(bookID.HasValue && userID.HasValue)
            {
                loanRepository.CreateLoan(bookID.Value, userID.Value);
                loanRepository.SaveAndDispose();

                return RedirectToAction("DisplayLoans");
            }
            return View("_Error");
        }

        [ChildActionOnly]
        public ActionResult CancelLoan(int? bookID, int? userID)
        {
            if (bookID.HasValue && userID.HasValue)
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