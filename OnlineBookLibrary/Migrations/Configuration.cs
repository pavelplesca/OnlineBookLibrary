using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OnlineBookLibrary.Models;
namespace OnlineBookLibrary.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            string name = "valeriuLibrarian@mail.com";

            string email = "valeriuLibrarian@mail.com";

            string role = "librarian";

            string password = "librarian";

            if (!roleManager.RoleExists(role))
            {
                roleManager.Create(new IdentityRole(role));
            }

            var user = new ApplicationUser();



            user.UserName = name;
            user.Email = email;
            if (!userManager.Users.Any(u => u.UserName == name))
            {
                var adminresult = userManager.Create(user, password);

                if (adminresult.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, role);
                }
            }

            context.Tags.AddOrUpdate(new Tag() { Id = 1, Name = "Java" });
            context.Tags.AddOrUpdate(new Tag() { Id = 2, Name = "C/C++" });
            context.Tags.AddOrUpdate(new Tag() { Id = 3, Name = "MVC" });
            context.Tags.AddOrUpdate(new Tag() { Id = 4, Name = "ASPNet" });
            context.Tags.AddOrUpdate(new Tag() { Id = 5, Name = "Programming" });
            context.Tags.AddOrUpdate(new Tag() { Id = 6, Name = "Artistic", });

            context.Books.AddOrUpdate(new Book
            {
                Id = 1,
                Name = "The Dark Tower",
                Author = "Stephen King",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam imperdiet.",
                Image = "DarkTower1.jpg",
                Year = 2018
            });


            context.Books.AddOrUpdate(new Book
            {
                Id = 2,
                Name = "Harry Potter and the sorcerer's stone",
                Author = "J. K. Rolling",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam imperdiet.",
                Image = "HarryPotter1.jpg",
                Year = 2018
            });



            context.Books.AddOrUpdate(new Book
            {
                Id = 3,
                Name = "Brief Story of Time",
                Author = "Stephen Hawking",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam imperdiet.",
                Image = "a_brief_history_of_time.jpg",
                Year = 2018
            });


            context.Books.AddOrUpdate(new Book
            {
                Id = 4,
                Name = "Game Of Thrones",
                Author = "G. R. R. Martin",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam imperdiet.",
                Image = "GameOfThrones.jpg",
                Year = 2018
            });


            context.Books.AddOrUpdate(new Book
            {
                Id = 5,
                Name = "Ready Player One",
                Author = "Ernest Cline",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam imperdiet.",
                Image = "ReadyPlayerOne.jpg",
                Year = 2018
            });



            context.Books.AddOrUpdate(new Book
            {
                Id = 6,
                Name = "C# In A Nutshell",
                Author = "Joseph Albahari",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam imperdiet.",
                Image = "CSharpInANutshell.jpg",
                Year = 2018
            });



            context.Books.AddOrUpdate(new Book
            {
                Id = 7,
                Name = "Java In A Nutshell",
                Author = "Author A. B.",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam imperdiet.",
                Image = "java.jpg",
                Year = 2018
            });


            context.Books.AddOrUpdate(new Book
            {
                Id = 8,
                Name = "Asp.NET 4",
                Author = "John Smith",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam imperdiet.",
                Image = "aspnet.jpg",
                Year = 2018
            });

            context.SaveChanges();
            //--------- Adding Tags in Books
            var book = context.Books.Include("Tags").FirstOrDefault(b => b.Name == "The Dark Tower");
            if (book != null)
            {
                if (book.Tags.All(t => t.Name != "Artistic"))
                    book.Tags.Add(context.Tags.FirstOrDefault(t => t.Name == "Artistic"));
            }

            book = context.Books.Include("Tags").FirstOrDefault(b => b.Name == "Harry Potter and the sorcerer's stone");
            if (book != null)
            {
                if (book.Tags.All(t => t.Name != "Artistic"))
                    book.Tags.Add(context.Tags.FirstOrDefault(t => t.Name == "Artistic"));
            }

            book = context.Books.Include("Tags").FirstOrDefault(b => b.Name == "Brief Story of Time");
            if (book != null)
            {
                if (book.Tags.All(t => t.Name != "Artistic"))
                    book.Tags.Add(context.Tags.FirstOrDefault(t => t.Name == "Artistic"));
            }

            book = context.Books.Include("Tags").FirstOrDefault(b => b.Name == "Game Of Thrones");
            if (book != null)
            {
                if (book.Tags.All(t => t.Name != "Artistic"))
                    book.Tags.Add(context.Tags.FirstOrDefault(t => t.Name == "Artistic"));
            }

            book = context.Books.Include("Tags").FirstOrDefault(b => b.Name == "Ready Player One");
            if (book != null)
            {
                if (book.Tags.All(t => t.Name != "Artistic"))
                    book.Tags.Add(context.Tags.FirstOrDefault(t => t.Name == "Artistic"));
            }


            book = context.Books.Include("Tags").FirstOrDefault(b => b.Name == "C# In A Nutshell");
            if (book != null)
            {
                if (book.Tags.All(t => t.Name != "Programming"))
                    book.Tags.Add(context.Tags.FirstOrDefault(t => t.Name == "Programming"));
            }

            book = context.Books.Include("Tags").FirstOrDefault(b => b.Name == "Java In A Nutshell");
            if (book != null)
            {
                if (book.Tags.All(t => t.Name != "Programming"))
                    book.Tags.Add(context.Tags.FirstOrDefault(t => t.Name == "Programming"));
                if (book.Tags.All(t => t.Name != "Java"))
                    book.Tags.Add(context.Tags.FirstOrDefault(t => t.Name == "Java"));
            }

            book = context.Books.Include("Tags").FirstOrDefault(b => b.Name == "Asp.NET 4");
            if (book != null)
            {
                if (book.Tags.All(t => t.Name != "Programming"))
                    book.Tags.Add(context.Tags.FirstOrDefault(t => t.Name == "Programming"));
                if (book.Tags.All(t => t.Name != "ASPNet"))
                    book.Tags.Add(context.Tags.FirstOrDefault(t => t.Name == "ASPNet"));
            }



            context.SaveChanges();
        }
    }
}
