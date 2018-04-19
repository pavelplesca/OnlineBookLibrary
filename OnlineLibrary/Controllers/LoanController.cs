using OnlineLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineLibrary.Controllers
{
    public class LoanController : Controller
    {
        //private OnlineLibraryDb context = new OnlineLibraryDb();

        public List<TestUser> users = new List<TestUser>
        {
            new TestUser(1, "Chris", null),
            new TestUser(2, "John", null),
            new TestUser(3, "Shaniqua", null)
        };

        public List<Loan> loans = new List<Loan>
        {
            new Loan(1, DateTime.Parse("2009/02/26 18:37:58"), DateTime.Now,
                DateTime.MinValue, Status.NowRenting, 0, null),
            new Loan(2, DateTime.Parse("2009/02/26 18:37:58"), DateTime.Now,
                DateTime.MinValue, Status.NowRenting, 0, null),
            new Loan(3, DateTime.Parse("2009/02/26 18:37:58"), DateTime.Now,
                DateTime.MinValue, Status.NowRenting, 0, null),
        };

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DisplayLoans()
        {
            loans[0].BookID = 1;
            loans[0].Book = new Book
            {
                Author = "Chris Nirones",
                Id = 1,
                Name = "How to survive life",
                Year = 2018
            };

            loans[1].BookID = 2;
            loans[1].Book = new Book
            {
                Author = "Chris Nirones",
                Id = 2,
                Name = "How to survive life part 2",
                Year = 2018
            };

            loans[2].BookID = 3;
            loans[2].Book = new Book
            {
                Author = "Chris Nirones",
                Id = 3,
                Name = "How not to survive life",
                Year = 2018
            };

            users[0].Loans = new List<Loan>
            {
                loans[0]
            };

            users[1].Loans = new List<Loan>
            {
                loans[1]
            };

            users[2].Loans = new List<Loan>
            {
                loans[2]
            };

            return View(loans);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    base.Dispose(disposing);
        //    context.Dispose();
        //}
    }
}