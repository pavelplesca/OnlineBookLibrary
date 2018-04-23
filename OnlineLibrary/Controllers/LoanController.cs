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
            return RedirectToAction("DisplayLoans");
        }

        [HttpGet]
        public ActionResult DisplayLoans()
        {       
            return View();
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult DisplayHistory(int userId)
        {
            ICollection<Loan> loanHistory = loanRepository.ReturnLoanHistory(userId).ToList();

            if(loanHistory.Count != 0)
            {
                return PartialView("_LoanHistoryInformation", loanHistory);
            }

            return PartialView("_LoanHistoryInfoEmpty");          
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult DisplayActiveLoan(int userId)
        {
            Loan activeLoan = loanRepository.ReturnActiveLoan(userId);

            if (activeLoan != null)
            {
                return PartialView("_ActiveLoanBookDetails", activeLoan);
            }

            return PartialView("_ActiveLoanNoBook");
        }
        
        [HttpGet]
        public ActionResult CreateLoan(int bookId, int userId)
        {
            loanRepository.CreateLoan(bookId, userId);
            loanRepository.SaveAndDispose();

            return RedirectToAction("DisplayLoans");            
        }

        [HttpGet]
        public ActionResult CancelLoan(int bookId, int userId)
        {
            loanRepository.CancelLoan(bookId, userId);
            loanRepository.SaveAndDispose();

            return RedirectToAction("DisplayLoans");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}