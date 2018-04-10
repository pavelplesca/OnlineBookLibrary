using OnlineLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineLibrary.Controllers
{
    public class HomeController : Controller
    {
        private OnlineLibraryDb _db = new OnlineLibraryDb();
        // GET: Home
        public ActionResult Index()
        {
            var Books = _db.Books.ToList(); 
            return View(Books);
        }


        protected override void Dispose(bool disposing)
        {
            if (_db != null)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult DropDb()
        {
            _db.Database.Delete();
            return Content("Db is dropped");
        }

        public ActionResult FillDb()
        {
            _db.Books.RemoveRange(_db.Books.ToList());

            _db.Books.Add(new Book
            {
                Id = 1,
                Name = "The Dark Tower",
                Author = "Stephen King",
                Description = "Blablabla",
                Image = "DarkTower1.jpg",
                Year = 2018
            });

            _db.Books.Add(new Book
            {
                Id = 2,
                Name = "Harry Potter and the sorcerer's stone",
                Author = "J. K. Rolling",
                Description = "Blablabla",
                Image = "HarryPotter1.jpg",
                Year = 2018
            });
            _db.Books.Add(new Book
            {
                Id = 3,
                Name = "Brief Story of Time",
                Author = "Stephen Hawking",
                Description = "Blablabla",
                Image = "a_brief_history_of_time.jpg",
                Year = 2018
            });
            _db.Books.Add(new Book
            {
                Id = 4,
                Name = "Game Of Thrones",
                Author = "G. R. R. Martin",
                Description = "Blablabla",
                Image = "GameOfThrones.jpg",
                Year = 2018
            });

            _db.Books.Add(new Book
            {
                Id = 5,
                Name = "Ready Player One",
                Author = "Ernest Cline",
                Description = "Blablabla",
                Image = "ReadyPlayerOne.jpg",
                Year = 2018
            });

            _db.Books.Add(new Book
            {
                Id = 6,
                Name = "CSharpInANutshell",
                Author = "Joseph Albahari",
                Description = "Blablabla",
                Image = "CSharpInANutshell.jpg",
                Year = 2018
            });

            _db.SaveChanges();
            
            return Content("Done!");
        }

    }
}