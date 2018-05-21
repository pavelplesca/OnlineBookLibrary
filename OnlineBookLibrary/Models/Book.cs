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
        [Required]
        [Display(Name ="Title")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Author(s)")]
        public string Author { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        public string Description { get; set; }
        public string Image { get; set; }
        [Required]
        public int Year { get; set; }
        public BookStatus Status { get; set; }

        [Required]
        public ICollection<Tag> Tags { get; set; }

        public Book()
        {
            Tags = new List<Tag>();
        }
    }
}