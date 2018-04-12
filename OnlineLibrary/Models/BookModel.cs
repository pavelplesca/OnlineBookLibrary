using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineLibrary.Models
{
    public class BookModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string Image { get; set; }

        public List<BookModel> GetDummyData()
        {
            return new List<BookModel>()
            {
                new BookModel()
                {
                    Name = "Leaving Bythe River",
                    Description = "Lorem ipsum",
                    Year = 1997,
                    Image = "images.carte01.jpg",
                },
                new BookModel()
                {
                    Name = "The Rowan Tree",
                    Description = "Lorem ipsum",
                    Year = 2001,
                    Image = "images.carte02.jpg",
                },
                new BookModel()
                {
                    Name = "Hasley Street",
                    Description = "Lorem ipsum",
                    Year = 2007,
                    Image = "images.carte03.jpg",
                }
            };
        }
    }
}