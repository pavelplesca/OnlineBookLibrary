using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnlineLibrary.Models
{
    public class OnlineLibraryDb : DbContext
    {
        public OnlineLibraryDb()
        {
            this.Database.Connection.ConnectionString =
                ConfigurationManager.ConnectionStrings["onlinelibrary2018"].ConnectionString;
        }

        public DbSet<Book> Books { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}