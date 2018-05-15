using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBookLibrary.Models.Repositories
{
    public class BookRepository : IDisposable
    {
        private readonly ApplicationDbContext context;

        public BookRepository()
        {
            context = new ApplicationDbContext();
        }

        public void Dispose()
        {
            if (context != null) context.Dispose();
        }
    }
}