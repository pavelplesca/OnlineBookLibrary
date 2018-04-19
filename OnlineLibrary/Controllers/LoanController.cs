using OnlineLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using OnlineLibrary.Persistence.Repositories;

namespace OnlineLibrary.Controllers
{
    public class LoanController : Controller
    {
        private LoanRepository loanRepository;

        public LoanController()
        {
            loanRepository = new LoanRepository();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DisplayLoans()
        {           
            return View();
        }

        public ActionResult CreateLoan(Book book)
        {
            return RedirectToAction("DisplayLoans");
        }

        public ActionResult CancelLoan(Book book)
        {
            return RedirectToAction("DisplayLoans"); ;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}