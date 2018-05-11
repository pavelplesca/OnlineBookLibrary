using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using OnlineBookLibrary.Models;
using OnlineBookLibrary.Persistence.Repositories;

namespace OnlineBookLibrary.Controllers
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
            ICollection<Loan> loanHistory = loanRepository.ReturnLoanHistory(userId);

            if (loanHistory.Count != 0)
            {
                return PartialView("~/Views/Loan/LoanHistoryPartials/_LoanHistoryInformation.cshtml", loanHistory);
            }

            return PartialView("~/Views/Loan/LoanHistoryPartials/_LoanHistoryInfoEmpty.cshtml");
        }

        [ChildActionOnly]
        public ActionResult DisplayActiveLoan(string userId)
        {
            Loan activeLoan = loanRepository.ReturnActiveLoan(userId);

            if (activeLoan != null)
            {
                return PartialView("~/Views/Loan/ActiveLoanPartials/_ActiveLoanBookDetails.cshtml", activeLoan);
            }

            return PartialView("~/Views/Loan/ActiveLoanPartials/_ActiveLoanNoBook.cshtml");
        }

        public ActionResult CreateLoan(int bookId, string userId)
        {
            loanRepository.CreateLoan(bookId, userId);

            return RedirectToAction("DisplayLoans");
        }

        public ActionResult CancelLoan(int bookId, string userId)
        {
            loanRepository.CancelLoan(bookId, userId);
            loanRepository.CheckIfNeedsBan(userId);

            return RedirectToAction("DisplayLoans");
        }

        public ActionResult CheckIfUserRentsBook(string userId, Book book)
        {
            if (!loanRepository.UserHasActiveRent(userId))
            {
                if (book.Status == BookStatus.Available)
                {
                    return PartialView("~/Views/Loan/ButtonDisplayPartials/_RentButton.cshtml", book);
                }
            }
            return new EmptyResult();
        }

        public ActionResult CheckCurrentLoan(string userId)
        {
            if (loanRepository.UserHasActiveRent(userId) &&
                loanRepository.IsCurrentLoanViolated(userId))
            {
                return PartialView("_ViolationWarning");
            }

            return new EmptyResult();
        }

        public ActionResult CheckUserBanStatus(string userId)
        {
            loanRepository.CheckUserBanStatus(userId);

            return new EmptyResult();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            loanRepository.SaveAndDispose();
        }
    }
}