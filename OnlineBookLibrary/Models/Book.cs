using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineBookLibrary.Models
{
    public enum BookStatus
    {
        Available,
        Rented
    }

    public class Book
    {
        public int Id { get; set; }
        [Display(Name ="Title")]
        public string Name { get; set; }
        [Display(Name = "Author(s)")]
        public string Author { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string Image { get; set; }
        public int Year { get; set; }
        public BookStatus Status { get; set; }

        public ICollection<Tag> Tags { get; set; }
        public Book()
        {
            Tags = new List<Tag>();
        }
    }
}