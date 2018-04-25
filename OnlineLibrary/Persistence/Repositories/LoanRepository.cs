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
            canceledLoan.Book.Status = BookStatus.Available;
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
    }
}