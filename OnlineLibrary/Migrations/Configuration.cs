using System.Data.Entity;

namespace OnlineLibrary.Migrations
{
    using OnlineLibrary.Models;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OnlineLibrary.Models.OnlineLibraryDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "OnlineLibrary.Models.OnlineLibraryDb";
        }

        protected override void Seed(OnlineLibrary.Models.OnlineLibraryDb context)
        {
            //  This method will be called after migrating to the latest version.

            context.Tags.AddOrUpdate(x => x.Name, new Tag() { Id = 1, Name = "Java" });
            context.Tags.AddOrUpdate(x => x.Name, new Tag() { Id = 2, Name = "C/C++" });
            context.Tags.AddOrUpdate(x => x.Name, new Tag() { Id = 3, Name = "MVC" });
            context.Tags.AddOrUpdate(x => x.Name, new Tag() { Id = 4, Name = "ASPNet" });
            context.Tags.AddOrUpdate(x => x.Name, new Tag() { Id = 5, Name = "Programming" });
            context.Tags.AddOrUpdate(x => x.Name, new Tag() { Id = 6, Name = "Artistic", });
            
            context.Books.AddOrUpdate(
                x => x.Name,
                new Book
                {
                    Id=1,
                    Name = "The Dark Tower",
                    Author = "Stephen King",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam imperdiet.",
                    Image = "DarkTower1.jpg",
                    Year = 2018
                });
            
           
            context.Books.AddOrUpdate(
                x => x.Name, 
                new Book
                {
                    Id = 2,
                    Name = "Harry Potter and the sorcerer's stone",
                    Author = "J. K. Rolling",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam imperdiet.",
                    Image = "HarryPotter1.jpg",
                    Year = 2018
                });
           


            context.Books.AddOrUpdate(
                x => x.Name,
                new Book
                {
                    Id = 3,
                    Name = "Brief Story of Time",
                    Author = "Stephen Hawking",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam imperdiet.",
                    Image = "a_brief_history_of_time.jpg",
                    Year = 2018
                });
            

            context.Books.AddOrUpdate(
                x => x.Name,
                new Book
                {
                    Id = 4,
                    Name = "Game Of Thrones",
                    Author = "G. R. R. Martin",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam imperdiet.",
                    Image = "GameOfThrones.jpg",
                    Year = 2018
                });

            
            context.Books.AddOrUpdate(
                x => x.Name,
                new Book
                {
                    Id = 5,
                    Name = "Ready Player One",
                    Author = "Ernest Cline",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam imperdiet.",
                    Image = "ReadyPlayerOne.jpg",
                    Year = 2018
                });
            


            context.Books.AddOrUpdate(
                x => x.Name,
                new Book
                {
                    Id = 6,
                    Name = "C# In A Nutshell",
                    Author = "Joseph Albahari",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam imperdiet.",
                    Image = "CSharpInANutshell.jpg",
                    Year = 2018
                });
           


            context.Books.AddOrUpdate(
                x => x.Name,
                new Book
                {
                    Id = 7,
                    Name = "Java In A Nutshell",
                    Author = "Author A. B.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam imperdiet.",
                    Image = "java.jpg",
                    Year = 2018
                });
            

            context.Books.AddOrUpdate(
                x => x.Name,
                new Book
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
            var book = context.Books.FirstOrDefault(b => b.Name == "The Dark Tower");
            if (book != null)
            {
                if (book.Tags.All(t => t.Name != "Artistic"))
                book.Tags.Add(context.Tags.FirstOrDefault(t => t.Name == "Artistic"));
            }

            book = context.Books.FirstOrDefault(b => b.Name == "Harry Potter and the sorcerer's stone");
            if (book != null)
            {
                if (book.Tags.All(t => t.Name != "Artistic"))
                    book.Tags.Add(context.Tags.FirstOrDefault(t => t.Name == "Artistic"));
            }

            book = context.Books.FirstOrDefault(b => b.Name == "Brief Story of Time");
            if (book != null)
            {
                if (book.Tags.All(t => t.Name != "Artistic"))
                    book.Tags.Add(context.Tags.FirstOrDefault(t => t.Name == "Artistic"));
            }

            book = context.Books.FirstOrDefault(b => b.Name == "Game Of Thrones");
            if (book != null)
            {
                if (book.Tags.All(t => t.Name != "Artistic"))
                    book.Tags.Add(context.Tags.FirstOrDefault(t => t.Name == "Artistic"));
            }

            book = context.Books.FirstOrDefault(b => b.Name == "Ready Player One");
            if (book != null)
            {
                if (book.Tags.All(t => t.Name != "Artistic"))
                    book.Tags.Add(context.Tags.FirstOrDefault(t => t.Name == "Artistic"));
            }


            book = context.Books.FirstOrDefault(b => b.Name == "C# In A Nutshell");
            if (book != null)
            {
                if (book.Tags.All(t => t.Name != "Programming"))
                    book.Tags.Add(context.Tags.FirstOrDefault(t => t.Name == "Programming"));
            }

            book = context.Books.FirstOrDefault(b => b.Name == "Java In A Nutshell");
            if (book != null)
            {
                if (book.Tags.All(t => t.Name != "Programming"))
                    book.Tags.Add(context.Tags.FirstOrDefault(t => t.Name == "Programming"));
                if (book.Tags.All(t => t.Name != "Java"))
                    book.Tags.Add(context.Tags.FirstOrDefault(t => t.Name == "Java"));
            }

            book = context.Books.FirstOrDefault(b => b.Name == "Asp.NET 4");
            if (book != null)
            {
                if (book.Tags.All(t => t.Name != "Programming"))
                    book.Tags.Add(context.Tags.FirstOrDefault(t => t.Name == "Programming"));
                if (book.Tags.All(t => t.Name != "ASPNet"))
                    book.Tags.Add(context.Tags.FirstOrDefault(t => t.Name == "ASPNet"));
            }
            context.SaveChanges();

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
