using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OnlineBookLibrary.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Loan> Loans { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class ApplicationUser : IdentityUser
    {
        public DateTime? BannedUntil { get; set; }
        public bool IsBanned { get; set; }
        public int ViolationsNr { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public enum BookStatus
    {
        Available,
        Rented
    }
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Year { get; set; }
        public BookStatus Status { get; set; }

        public ICollection<Tag> Tags { get; set; }
        public Book()
        {
            Tags = new List<Tag>();
        }
    }

    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
        public Tag()
        {
            Books = new List<Book>();
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public enum Status
    {
        NowRenting,
        Rented,
        Violated
    }
    public class Loan
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public DateTime BorrowDate { get; set; }

        public DateTime? ReturnedDate { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public int BookID { get; set; }
        public Book Book { get; set; }

        [Required]
        public string UserID { get; set; }
        public ApplicationUser User { get; set; }
    }


}