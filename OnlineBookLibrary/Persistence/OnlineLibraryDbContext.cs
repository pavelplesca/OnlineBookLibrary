using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using OnlineBookLibrary.Helpers;
using OnlineBookLibrary.Models;

namespace OnlineBookLibrary.Persistence
{
    public class OnlineLibraryDbContext : IdentityDbContext<User>
    {
        public OnlineLibraryDbContext()
            : base("onlinelibrary2018", throwIfV1Schema: false)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Loan> Loans { get; set; }

        public static OnlineLibraryDbContext Create()
        {
            return new OnlineLibraryDbContext();
        }
    }
}