using OnlineLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OnlineLibrary.Persistence.Repositories
{
    public class LoanRepository
    {
        private const int banDays = 7;
        private readonly OnlineLibraryDb context;

        public LoanRepository()
        {
            context = new OnlineLibraryDb();
        }      

        public void SaveAndDispose()
        {
            context.SaveChanges();
            context.Dispose();
        }

        public void CreateLoan(int bookId, string userId)
        {
            context.Loans.Add(
                new Loan
                {
                    BorrowDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(7),
                    ReturnedDate = null,
                    Status = Status.NowRenting,
                    BookID = bookId,
                    UserID = userId
                });

            Book book = context.Books.Where(x => x.Id == bookId).SingleOrDefault();
            book.Status = BookStatus.Rented;
        }

        public void CancelLoan(int bookId, string userId)
        {
            Loan canceledLoan = context.Loans
                .Where(x => x.BookID == bookId && x.UserID == userId && x.Status == Status.NowRenting)
                .SingleOrDefault();

            canceledLoan.Status = Status.Rented;
            canceledLoan.ReturnedDate = DateTime.Now;

            Book book = context.Books.Where(x => x.Id == bookId).SingleOrDefault();
            book.Status = BookStatus.Available;
        }

        public IList<Loan> ReturnLoanHistory(string userId)
        {
            return context.Loans
                .Where(x => x.UserID == userId && x.Status != Status.NowRenting)
                .Include(z => z.Book)
                .Include(y => y.Book.Tags)
                .ToList();
        }

        public Loan ReturnActiveLoan(string userId)
        {
            return context.Loans
                .Where(x => x.UserID == userId && x.Status == Status.NowRenting)
                .Include(z => z.Book)
                .Include(y => y.Book.Tags)
                .SingleOrDefault();
        }

        public bool CheckIfUserRentsBook(string userId, int bookId)
        {
            return context.Loans.Any(x => x.UserID == userId && x.BookID == bookId && x.Status == Status.NowRenting);
        }

        public bool UserHasActiveRent(string userId)
        {
            return context.Loans.Any(x => x.UserID == userId && x.Status == Status.NowRenting);
        }

        public int GetUserViolationNr(string userId)
        {
            return context.Users.Where(x => x.Id == userId)
                .SingleOrDefault()
                .ViolationsNr;
        }

        public void BanUser(string userId)
        {
            User user = context.Users
                .Where(x => x.Id == userId)
                .SingleOrDefault();

            Loan violatedLoan = context.Loans
                .Where(x => x.UserID == userId && x.Status == Status.Violated)
                .OrderByDescending(y => y.DueDate)
                .FirstOrDefault();

            user.IsBanned = true;
            user.BannedUntil = violatedLoan.DueDate.AddDays(banDays);
        }

        public bool IsCurrentLoanViolated(string userId)
        {
            Loan loan = context.Loans
                .Where(x => x.UserID == userId && x.Status == Status.NowRenting)
                .Include(z => z.User)
                .SingleOrDefault();

            if(loan.DueDate < DateTime.Now)
            {
                loan.User.ViolationsNr++;
                return true;
            }

            return false;
        }
    }
}