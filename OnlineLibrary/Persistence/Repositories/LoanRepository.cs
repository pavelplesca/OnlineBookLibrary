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

        public void CreateLoan(int bookID, int userID)
        {
            context.Loans.Add(
                new Loan
                {
                    BorrowDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(7),
                    ReturnedDate = null,
                    Status = Status.NowRenting,
                    BookID = bookID,
                    UserID = userID
                });
        }

        public void CancelLoan(int bookID, int userID)
        {
            Loan canceledLoan = context.Loans.Where(x => x.BookID == bookID && x.UserID == userID).SingleOrDefault();
            context.Loans.Remove(canceledLoan);
        }

        public IQueryable<Loan> ReturnLoanHistory(int userID)
        {
            return context.Loans.Where(x => x.UserID == userID && x.Status != Status.NowRenting).Include(z => z.Book).Include(y => y.Book.Tags);
        }

        public Loan ReturnActiveLoan(int userID)
        {
            return context.Loans.Where(x => x.UserID == userID && x.Status == Status.NowRenting).Include(z => z.Book).Include(y => y.Book.Tags).SingleOrDefault();
        }
    }
}