﻿using OnlineBookLibrary.Controllers.Attributes;
using OnlineBookLibrary.Models;
using OnlineBookLibrary.Persistence.Repositories;
using System;
using System.Collections.Generic;
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
            if (ModelState.IsValid)
            {
                var newTag = new Tag()
                {
                    Name = tag.Name
                };
                if (bookRepository.GetTags().Any(x => x.Name == tag.Name))
                    ModelState.AddModelError("", "This Tag already exists");
                else
                    bookRepository.AddTag(newTag);
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


    }
}