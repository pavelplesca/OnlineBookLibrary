namespace OnlineLibrary.Migrations
{
    using OnlineLibrary.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
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
            context.Books.AddOrUpdate(
                new Book
                {
                    Id = 1,
                    Name = "The Dark Tower",
                    Author = "Stephen King",
                    Description = "Blablabla",
                    Image = "DarkTower1.jpg",
                    Year = 2018,
                    Tags = new List<Tag>()
                        {
                            context.Tags.Find(6)
                        }
                },

                new Book
                {
                    Id = 2,
                    Name = "Harry Potter and the sorcerer's stone",
                    Author = "J. K. Rolling",
                    Description = "Blablabla",
                    Image = "HarryPotter1.jpg",
                    Year = 2018,
                    Tags = new List<Tag>()
                        {
                            context.Tags.Find(6)
                        }
                },

                new Book
                {
                    Id = 3,
                    Name = "Brief Story of Time",
                    Author = "Stephen Hawking",
                    Description = "Blablabla",
                    Image = "a_brief_history_of_time.jpg",
                    Year = 2018,
                    Tags = new List<Tag>()
                    {
                        context.Tags.Find(6)
                    }
                },

                new Book
                {
                    Id = 4,
                    Name = "Game Of Thrones",
                    Author = "G. R. R. Martin",
                    Description = "Blablabla",
                    Image = "GameOfThrones.jpg",
                    Year = 2018,
                    Tags = new List<Tag>()
                    {
                        context.Tags.Find(6)
                    }
                },

                new Book
                {
                    Id = 5,
                    Name = "Ready Player One",
                    Author = "Ernest Cline",
                    Description = "Blablabla",
                    Image = "ReadyPlayerOne.jpg",
                    Year = 2018,
                    Tags = new List<Tag>()
                    {
                        context.Tags.Find(6)
                    }
                },

                new Book
                {
                    Id = 6,
                    Name = "C# In A Nutshell",
                    Author = "Joseph Albahari",
                    Description = "Blablabla",
                    Image = "CSharpInANutshell.jpg",
                    Year = 2018,
                    Tags = new List<Tag>()
                    {
                        context.Tags.Find(5)
                    }
                },

                new Book
                {
                    Id = 7,
                    Name = "Java In A Nutshell",
                    Author = "Author A. B.",
                    Description = "Blablabla",
                    Image = "java.jpg",
                    Year = 2018,
                    Tags = new List<Tag>()
                    {
                        context.Tags.Find(5),
                        context.Tags.Find(1)
                    }
                },

                new Book
                {
                    Id = 8,
                    Name = "Asp.NET 4",
                    Author = "John Smith",
                    Description = "Blablabla",
                    Image = "aspnet.jpg",
                    Year = 2018,
                    Tags = new List<Tag>()
                    {
                        context.Tags.Find(5),
                        context.Tags.Find(4)
                    }
                }
                );
        }
    }
}
