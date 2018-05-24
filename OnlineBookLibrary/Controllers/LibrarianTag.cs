using OnlineBookLibrary.Controllers.Attributes;
using OnlineBookLibrary.Models;
using OnlineBookLibrary.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineBookLibrary.Controllers
{
    [Authorize(Roles = "librarian")]
    public partial class LibrarianController : Controller
    {

        IBookRepository bookRepository;

        public LibrarianController(IBookRepository r)
        {
            bookRepository = r;
        }

        public ActionResult AddTag()
        {

            return PartialView("_AddTag");
        }

        [HttpPost]
        public ActionResult AddTag(Tag tag)
        {
            var tags = bookRepository.GetTags();
            if (tags.Any(x => x.Name.ToUpper() == tag.Name?.ToUpper()))
                ModelState.AddModelError("Name", "This Tag already exists");
            if (ModelState.IsValid)
            {
                var newTag = new Tag()
                {
                    Name = tag.Name
                };
                
                    bookRepository.AddTag(newTag);
            }
            else
            {
                Response.StatusCode = 302;
            }
            
            var result = PartialView("_AddTag",tag);
            return result;
        }

        [HttpPost]
        public ActionResult RemoveTag(Tag tag)
        {
            Tag myTag = bookRepository.GetTag(tag.Id);
            if (myTag == null)
            {
                return Content("fail");
            }
            else
            {
                bookRepository.RemoveTag(myTag);
            }
            return Content("");
                
        }
        [HttpPost]
        public ActionResult EditTag(Tag tag)
        {
            
            if (bookRepository.GetTags().Any(x => x.Name.ToUpper() == tag.Name?.ToUpper()))
                return Content("Duplicate value");
            if (!ModelState.IsValid)
            {
                return Content("Form not valid!");
            }
            Tag oldTag = bookRepository.GetTag(tag.Id);
            if (oldTag == null)
            {
                return Content("Sorry,couldn't find this tag");
            }
            try
            {
                bookRepository.EditTag(tag);
            }
            catch (EntityException)
            {
                return Content("Sorry,we couldn't update this tag,please try again later");
            }
            return Content("");
        }


    }
}