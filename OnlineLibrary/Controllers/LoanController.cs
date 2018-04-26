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
        private const int maxViolations = 5;

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

            return RedirectToAction("DisplayLoans");
        }

        public ActionResult CheckIfUserRentsBook(string userId, Book book)
        {
            bool isReceivedBookRentedByUser = loanRepository.CheckIfUserRentsBook(userId, book.Id);

            if (!isReceivedBookRentedByUser)
            {
                if(!loanRepository.UserHasActiveRent(userId))
                {
                    if(book.Status == BookStatus.Available)
                    {
                        return PartialView("~/Views/Loan/ButtonDisplayPartials/_UserIsNotRentingButtons.cshtml", book);
                    }
                }
                return new EmptyResult();
            }
            else 
            {
                return PartialView("~/Views/Loan/ButtonDisplayPartials/_UserIsRentingButtons.cshtml", book);
            }
        }

        public ActionResult CheckUserViolationsNr(string userId)
        {
            int violationsNr = loanRepository.GetUserViolationNr(userId);

            if(violationsNr == maxViolations)
            {
                loanRepository.BanUser(userId);
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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            loanRepository.SaveAndDispose();
        }
    }
}