using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBookLibrary.Models
{
    public class Tag
    {
        public int Id { get; set; }
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