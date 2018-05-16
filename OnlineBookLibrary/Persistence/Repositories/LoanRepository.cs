using OnlineBookLibrary.Models;
using OnlineBookLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OnlineBookLibrary.Persistence.Repositories
{
    public interface ILoanRepository
    {
        void CheckIfNeedsBan(string userId);
        void CreateLoan(int bookId, string userId);
        void CancelLoan(int bookId, string userId);
        IList<Loan> ReturnLoanHistory(string userId);
        Loan ReturnActiveLoan(string userId);
        bool UserHasActiveRent(string userId);
        bool IsCurrentLoanViolated(string userId);
        void CheckUserBanStatus(string userId);
    }

    public class LoanRepository: IDisposable, ILoanRepository
    {
        private const int banDays = 7;
        private const int daysPerLoan = 7;
        private const int maxViolations = 5;

        private readonly OnlineLibraryDbContext context;

        public LoanRepository()
        {
            context = new OnlineLibraryDbContext();
        }

        public void SaveAndDispose()
        {
            context.SaveChanges();
        }

        public void CreateLoan(int bookId, string userId)
        {
            bool loanAlreadyExists = context.Loans
                .Any(l => l.Status == Status.NowRenting && l.BookID == bookId && l.UserID == userId);

            if(!loanAlreadyExists)
            {
                context.Loans.Add(
                new Loan
                {
                    BorrowDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(daysPerLoan),
                    ReturnedDate = null,
                    Status = Status.NowRenting,
                    BookID = bookId,
                    UserID = userId
                });

                Book book = context.Books.Where(x => x.Id == bookId).SingleOrDefault();
                book.Status = BookStatus.Rented;
            }
            context.SaveChanges();
        }

        public void CancelLoan(int bookId, string userId)
        {
            Loan canceledLoan = context.Loans
                .Where(x => x.BookID == bookId && x.UserID == userId && (x.Status == Status.NowRenting || x.Status == Status.Violated))
                .SingleOrDefault();

            canceledLoan.Status = Status.Rented;
            canceledLoan.ReturnedDate = DateTime.Now;

            Book book = context.Books.Where(x => x.Id == bookId).SingleOrDefault();
            book.Status = BookStatus.Available;
            context.SaveChanges();
        }

        public IList<Loan> ReturnLoanHistory(string userId)
        {
            return context.Loans
                .Where(x => x.UserID == userId && x.Status != Status.NowRenting && x.Status != Status.Violated)
                .Include(z => z.Book)
                .ToList();
        }

        public Loan ReturnActiveLoan(string userId)
        {
            return context.Loans
                .Where(x => x.UserID == userId && (x.Status == Status.NowRenting || x.Status == Status.Violated))
                .Include(y => y.Book.Tags)
                .SingleOrDefault();
        }

        public bool CheckIfUserRentsBook(string userId, int bookId)
        {
            return context.Loans.Any(x => x.UserID == userId && x.BookID == bookId && (x.Status == Status.NowRenting || x.Status == Status.Violated));
        }

        public bool UserHasActiveRent(string userId)
        {
            return context.Loans.Any(x => x.UserID == userId && (x.Status == Status.NowRenting || x.Status == Status.Violated));
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
                .Where(x => x.UserID == userId)
                .OrderByDescending(y => y.BorrowDate)
                .FirstOrDefault();

            user.IsBanned = true;
            user.BannedUntil = violatedLoan.ReturnedDate.Value.AddDays(banDays);
            violatedLoan.Status = Status.Rented;
        }

        public bool IsCurrentLoanViolated(string userId)
        {
            Loan loan = context.Loans
                .Where(x => x.UserID == userId && x.Status == Status.Violated)
                .Include(z => z.User)
                .SingleOrDefault();

            if(loan != null && loan.DueDate < DateTime.Now)
            {
                if(loan.User.ViolationsNr < maxViolations)
                {
                    loan.User.ViolationsNr++;
                }
                return true;
            }

            return false;
        }

        public void CheckUserBanStatus(string userId)
        {
            User user = context.Users
                .Where(x => x.Id == userId)
                .SingleOrDefault();

            if(user != null && user.IsBanned && DateTime.Now > user.BannedUntil.Value.Date)
            {
                UnbanUser(user);
            }        
        }

        private void UnbanUser(User user)
        {
            user.IsBanned = false;
            user.BannedUntil = null;
            user.ViolationsNr = 0;
        }

        public void CheckIfNeedsBan(string userId)
        {
            int violationsNr = GetUserViolationNr(userId);

            if (violationsNr == maxViolations)
            {
                BanUser(userId);
            }
        }

        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }
        }
    }
}