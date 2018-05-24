using OnlineBookLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBookLibrary.Models.ViewModels
{
    public class BookTagViewModel
    {
        public Book Book { get; set; }

        [TagValidator(1, ErrorMessage = "Select at least one tag.")]
        public IEnumerable<int> selectedTags { get; set; }
        public IEnumerable<Tag> AllTags { get; set; }
    }
}