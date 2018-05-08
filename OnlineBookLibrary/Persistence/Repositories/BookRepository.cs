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

        /*public IEnumerable<> BookDetails(int? id)
        {
            var book = _db.Books.Include(x => x.Tags).ToList().Where(x => x.Id == id).FirstOrDefault();
            if (book == null) return RedirectToAction("Index", "Home");
            return View(book);
        }*/

        public void Dispose()
        {
            if (context != null) context.Dispose();
        }
    }
}