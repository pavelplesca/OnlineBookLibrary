using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OnlineLibrary.Models
{
    public class OnlineLibraryDb : IdentityDbContext<User>// DbContext
    {
        public OnlineLibraryDb()
        {
            this.Database.Connection.ConnectionString =
                ConfigurationManager.ConnectionStrings["onlinelibrary2018"].ConnectionString;
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public static OnlineLibraryDb Create()
        {
            return new OnlineLibraryDb();
        }
    }
}