using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnlineLibrary.Models
{
    public class OnlineLibraryDb : DbContext
    {
        public DbSet<Book> Books { get; set; }
    }
}