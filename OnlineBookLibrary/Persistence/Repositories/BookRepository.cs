using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineBookLibrary.Models;

namespace OnlineBookLibrary.Persistence.Repositories
{
    public class BookRepository : IDisposable
    {
        private readonly OnlineLibraryDbContext context;

        public BookRepository()
        {
            context = new OnlineLibraryDbContext();
        }

        public Book GetBookDetailsById(int id)
        {
            return context.Books.Include("Tags").FirstOrDefault(b => b.Id == id);
        }

        public IEnumerable<Book> GetTopLoanedBooks(int count = 3)
        {
            /*var books = from b in context.Books
                join l in context.Loans
                    on b.Id equals l.BookID into j
                group j by b.Id into grouped
                select  new { bookId = grouped.Key, Count = grouped.Count(x =>x.)}
            */
            var LoanedBookGroups = context.Loans.GroupBy(x => x.BookID).OrderBy( x => x.Count()).Take(count);
            
            //var books = context.Books.Join()

            return LoanedBookGroups;
        }

        public void Dispose()
        {
            if (context != null) context.Dispose();
        }
    }
}