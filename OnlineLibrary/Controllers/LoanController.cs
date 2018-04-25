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
    [Authorize]
    public class LoanController : Controller
    {
        private LoanRepository loanRepository;

        public LoanController()
        {
            loanRepository = new LoanRepository();
        }

        public ActionResult Index()
        {
            return RedirectToAction("DisplayLoans");
        }

        public ActionResult DisplayLoans(string userId)
        {       
            return View();
        }

        [ChildActionOnly]
        public ActionResult DisplayHistory(string userId)
        {
            ICollection<Loan> loanHistory = loanRepository.ReturnLoanHistory(userId).ToList();

            if (loanHistory.Count != 0)
            {
                return PartialView("_LoanHistoryInformation", loanHistory);
            }

            return PartialView("_LoanHistoryInfoEmpty");
        }

        [ChildActionOnly]
        public ActionResult DisplayActiveLoan(string userId)
        {
            Loan activeLoan = loanRepository.ReturnActiveLoan(userId);

            if (activeLoan != null)
            {
                return PartialView("_ActiveLoanBookDetails", activeLoan);
            }

            return PartialView("_ActiveLoanNoBook");
        }

        public ActionResult CreateLoan(int bookId, string userId)
        {
            loanRepository.CreateLoan(bookId, userId);
            loanRepository.SaveAndDispose();

            return RedirectToAction("DisplayLoans");
        }

        public ActionResult CancelLoan(int bookId, string userId)
        {
            loanRepository.CancelLoan(bookId, userId);
            loanRepository.SaveAndDispose();

            return RedirectToAction("DisplayLoans");
        }

        public ActionResult CheckIfUserRentsBook(string userId, Book book)
        {
            bool bookIsRentedByUser = loanRepository.CheckIfUserRentsBook(userId, book.Id);

            if (!bookIsRentedByUser)
            {
                if(!loanRepository.UserHasActiveRent(userId))
                {
                    return PartialView("_UserIsNotRentingButtons", book);
                }
                return PartialView("_UserHasActiveLoanPartial");
            }
            else 
            {
                return PartialView("_UserIsRentingButtons", book);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}