using OnlineBookLibrary.Controllers.Attributes;
using OnlineBookLibrary.Models;
using OnlineBookLibrary.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
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
                return Json(new {Success = true});
            }

            var errorModel =
                    from x in ModelState.Keys
                    where ModelState[x].Errors.Count > 0
                    select new
                    {
                        key = x,
                        errors = ModelState[x].Errors.
                            Select(y => y.ErrorMessage).
                            ToArray()
                    };

            var result = new JsonResult()
            {
                Data = errorModel
            };
            Response.StatusCode = 301;
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