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
                ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<TestUser> TestUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}