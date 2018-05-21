using OnlineBookLibrary.Models;
using OnlineBookLibrary.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineBookLibrary.Persistence;

namespace OnlineBookLibrary.Controllers
{
    [Authorize(Roles = "librarian")]
    public partial class LibrarianController: Controller
    {
        public ActionResult AddBook()
        {
            var _db = new OnlineLibraryDbContext();
            ViewBag.TagId = new SelectList(_db.Tags, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult AddBook(HttpPostedFileBase file, Book model)
        {
            BookRepository bRepo = new BookRepository();

            if (!ModelState.IsValid)
            {
                return View();
            }

            // Save image in folder
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Content/Images/Books"), pic);
                file.SaveAs(path);
                Random rand = new Random();
                model.Image = pic;
            }
            else
                model.Image = "no_cover.jpg";

            model.Status = BookStatus.Available;
            bRepo.AddBook(model);

            return RedirectToAction("Index", "Librarian");
        }

        public ActionResult DeleteBook(int id)
        {
            BookRepository bRep = new BookRepository();

            bRep.DeleteBook(id);

            return RedirectToAction("Index", "Librarian");
        }
    }
}