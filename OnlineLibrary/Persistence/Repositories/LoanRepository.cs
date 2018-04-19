using OnlineLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineLibrary.Persistence.Repositories
{
    public class LoanRepository
    {
        private readonly OnlineLibraryDb context;

        public LoanRepository()
        {
            context = new OnlineLibraryDb();
        }      

        public void DisposeAndSave()
        {
            context.SaveChanges();
            context.Dispose();
        }
    }
}