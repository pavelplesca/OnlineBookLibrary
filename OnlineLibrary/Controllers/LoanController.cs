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

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DisplayLoans()
        {       
            return View();
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult DisplayHistory(int? userId)
        {
            if (userId.HasValue)
            {
                ICollection<Loan> loanHistory = loanRepository.ReturnLoanHistory(userId.Value).ToList();

                if(loanHistory.Count != 0)
                {
                    return PartialView("LoanHistoryPartial", loanHistory);
                }

                return PartialView("EmptyHistoryPartial");
            }

            return View("_Error");            
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult DisplayActiveLoan(int? userId)
        {
            if (userId.HasValue)
            {
                Loan activeLoan = loanRepository.ReturnActiveLoan(userId.Value);

                if (activeLoan != null)
                {
                    return PartialView("ActiveLoanPartial", activeLoan);
                }

                return PartialView("EmptyLoanPartial");
            }

            return View("_Error");
        }
        
        [HttpPost]
        public ActionResult CreateLoan(int? bookId, int? userId)
        {
            if(bookId.HasValue && userId.HasValue)
            {
                loanRepository.CreateLoan(bookId.Value, userId.Value);
                loanRepository.SaveAndDispose();

                return RedirectToAction("DisplayLoans");
            }
            return View("_Error");
        }

        [HttpPost]
        public ActionResult CancelLoan(int? bookId, int? userId)
        {
            if (bookId.HasValue && userId.HasValue)
            {
                loanRepository.CancelLoan(bookId.Value, userId.Value);
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