using OnlineBookLibrary.Models;
using OnlineBookLibrary.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineBookLibrary.Persistence;
using OnlineBookLibrary.Models.ViewModels;

namespace OnlineBookLibrary.Controllers
{
    [Authorize(Roles = "librarian")]
    public partial class LibrarianController: Controller
    {
        BookRepository bRepo = new BookRepository();

        public ActionResult AddBook()
        {
            ViewBag.ViewTitle = "Add New Book";
            
            BookTagViewModel bt = new BookTagViewModel();

            bt.AllTags = SelectAllTags();

            return View("AddEditBook", bt);
        }

        [HttpPost]
        public ActionResult AddBook(HttpPostedFileBase file, BookTagViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AllTags = SelectAllTags();

                ViewBag.ViewTitle = "Add New Book";
                return View("AddEditBook", model);
            }

            // Save image in folder
            if (file != null)
            {
                SaveImage(file, model.Book);
            }
            else
                model.Book.Image = "no_cover.jpg";

            model.Book.Status = BookStatus.Available;

            bRepo.AddBook(model);

            return RedirectToAction("Index", "Librarian");
        }
        private IEnumerable<Tag> SelectAllTags()
        {
            var tags = bRepo.GetTags().Select(t => new Tag
            {
                Id = t.Id, 
                Name = t.Name
            }).ToList();

            // Remove tag - "All"
            tags.RemoveAt(0);

            return tags;
        }

        public ActionResult EditBook(int id)
        {
            BookTagViewModel btmodel = new BookTagViewModel();

            Book model = bRepo.GetBookDetailsById(id);
            btmodel.Book = model;
            btmodel.AllTags = SelectAllTags();
            btmodel.selectedTags = model.Tags.Select(t => t.Id).ToList();

            ViewBag.ViewTitle = "Edit Book";
            return View("AddEditBook", btmodel);
        }

        [HttpPost]
        public ActionResult EditBook(HttpPostedFileBase file, BookTagViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Book.Image = bRepo.GetBookDetailsById(model.Book.Id).Image;

                model.AllTags = SelectAllTags();
                ViewBag.ViewTitle = "Edit Book";
                return View("AddEditBook", model);
            }

            // Save image in folder
            // TODO: Delete old file
            if (file != null)
            {
                SaveImage(file, model.Book);
            }

            bRepo.EditBook(model);
            return RedirectToAction("Index", "Librarian");
        }
        private void SaveImage(HttpPostedFileBase file, Book model)
        {
            Random rand = new Random();
            string prefix = rand.Next(int.MaxValue).ToString();
            string pic = prefix + System.IO.Path.GetFileName(file.FileName);
            string path = System.IO.Path.Combine(Server.MapPath("~/Content/Images/Books"), pic);
            file.SaveAs(path);

            model.Image = pic;
        }

        public ActionResult DeleteBook(int id)
        {
            bRepo.DeleteBook(id);

            return RedirectToAction("Index", "Librarian");
        }
    }
}