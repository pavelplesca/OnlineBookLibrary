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
        BookRepository bRepo = new BookRepository();

        public ActionResult AddBook()
        {
            ViewBag.ViewTitle = "Add New Book";
            // Get all tags
            var tags = SelectAllTags();

            ViewBag.Tags = new MultiSelectList(tags, "Id", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult AddBook(HttpPostedFileBase file, Book model, int[] tagId)
        {
            if (!ModelState.IsValid || tagId == null)
            {
                IEnumerable<Tag> tags = SelectAllTags(); ;

                if (tagId == null)
                {
                    ModelState.AddModelError("", "Select at least one tag.");
                    ViewBag.Tags = new MultiSelectList(tags, "Id", "Name");
                }
                else
                {
                    ViewBag.Tags = new MultiSelectList(tags, "Id", "Name", selectedValues: tags);
                }

                ViewBag.ViewTitle = "Add New Book";
                return View(model);
            }

            // Save image in folder
            if (file != null)
            {
                SaveImage(file, model);
            }
            else
                model.Image = "no_cover.jpg";

            model.Status = BookStatus.Available;
            bRepo.AddBook(model, tagId);

            return RedirectToAction("Index", "Librarian");
        }

        private IEnumerable<Tag> SelectAllTags()
        {
            var tags = bRepo.GetTags().Select(t => new Tag
            {
                Id = t.Id, 
                Name = t.Name,
            }).ToList();

            // Remove tag - "All"
            tags.RemoveAt(0);

            return tags;
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

        public ActionResult EditBook(int id)
        {
            ViewBag.ViewTitle = "Edit Book";
            return RedirectToAction("Index", "Librarian");
        }

        public ActionResult DeleteBook(int id)
        {
            bRepo.DeleteBook(id);

            return RedirectToAction("Index", "Librarian");
        }
    }
}