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
            BookRepository bRep = new BookRepository();

            // Get all tags
            var tags = bRep.GetTags().Select(t => new
            {
                TagId = t.Id,
                TagName = t.Name
            }).ToList();

            // Remove tag - "All"
            tags.RemoveAt(0);

            ViewBag.Tags = new MultiSelectList(tags, "TagId", "TagName");

            return View();
        }

        [HttpPost]
        public ActionResult AddBook(HttpPostedFileBase file, Book model, int[] tagId)
        {
            BookRepository bRepo = new BookRepository();

            if (!ModelState.IsValid)
            {
                var tags = bRepo.GetTags().Select(t => new
                {
                    TagId = t.Id,
                    TagName = t.Name,
                }).ToList();

                tags.RemoveAt(0);

                if(tagId == null)
                    ModelState.AddModelError("","Select at least one tag.");

                ViewBag.Tags = new MultiSelectList(tags, "TagId", "TagName");
                
                return View(model);
            }

            // Save image in folder
            if (file != null)
            {
                Random rand = new Random();
                string prefix = rand.Next(int.MaxValue).ToString();
                string pic = prefix + System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Content/Images/Books"), pic);
                file.SaveAs(path);

                model.Image = pic;
            }
            else
                model.Image = "no_cover.jpg";

            model.Status = BookStatus.Available;
            bRepo.AddBook(model, tagId);

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