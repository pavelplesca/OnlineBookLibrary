using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineBookLibrary.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        [StringLength(450)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
        public Tag()
        {
            Books = new List<Book>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}