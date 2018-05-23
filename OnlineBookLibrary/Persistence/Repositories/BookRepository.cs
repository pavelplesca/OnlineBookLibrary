using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Hosting;
using OnlineBookLibrary.Models;
using OnlineBookLibrary.Models.ViewModels;

namespace OnlineBookLibrary.Persistence.Repositories
{
    public interface IBookRepository
    {
        Book GetBookDetailsById(int id);
        IEnumerable<Book> GetTopLoanedBooks(int count = 3);
        IEnumerable<Book> GetPageOfBooks(IEnumerable<Book> books, int pageNum = 1, int booksOnPage = 6);
        IEnumerable<Book> GetBooksByTag(string tag = "All");
        IEnumerable<Tag> GetTags();
        void AddTag(Tag tag);
        void RemoveTag(Tag tag);
        Tag GetTag(int id);
        void EditTag(Tag tag);
    }

    public class BookRepository : IDisposable, IBookRepository
    {
        private readonly OnlineLibraryDbContext _context;

        public BookRepository()
        {
            _context = new OnlineLibraryDbContext();
        }
        
        public void AddBook(BookTagViewModel model)
        {
            ICollection<Tag> bookTags = new List<Tag>();

            var allTags = GetTags();

            foreach (int tagId in model.selectedTags)
            {
                bookTags.Add(allTags.Where(t => t.Id == tagId).SingleOrDefault());
            }

            Book newBook = new Book()
            {
                 Author = model.Book.Author,
                 Name = model.Book.Name,
                 Description = model.Book.Description,
                 Image = model.Book.Image,
                 Year = model.Book.Year,
                 Tags = bookTags
            };

            _context.Books.Add(newBook);

            _context.SaveChanges();
        }

        public void EditBook(BookTagViewModel model)
        {
            Book oldBook = _context.Books.Include("Tags").Where(b => b.Id == model.Book.Id).SingleOrDefault();
            ICollection<Tag> selectedTags = _context.Tags.Where( t => model.selectedTags.Contains(t.Id)).ToList();

            if (oldBook != null)
            {
                oldBook.Name = model.Book.Name;
                oldBook.Author = model.Book.Author;
                oldBook.Description = model.Book.Description;
                oldBook.Year = model.Book.Year;

                if (model.Book.Image != null)
                    oldBook.Image = model.Book.Image;

                oldBook.Tags.Clear();

                foreach (var tag in selectedTags)
                {
                    oldBook.Tags.Add(tag);
                }

                _context.SaveChanges();
            }
        }

        public void DeleteBook(int id)
        {
            Book book = _context.Books.Where(b => b.Id == id).SingleOrDefault();

            if (book != null)
            {
                if(book.Image != "no_cover.jpg")
                {
                    string imagePath = Path.Combine(HostingEnvironment.MapPath("~/Content/Images/Books"), book.Image);
                    File.Delete(imagePath);
                }

                _context.Books.Remove(book);

                _context.SaveChanges();
            }
        }

        public Book GetBookDetailsById(int id)
        {
            return _context.Books.Include("Tags").FirstOrDefault(b => b.Id == id);
        }

        public IEnumerable<Book> GetTopLoanedBooks(int count = 3)
        {
            var mostLoanedBooksIds = _context.Loans
                .Where(x => DbFunctions.DiffDays(x.BorrowDate,DateTime.Today ) < 30)
                .GroupBy(x => x.BookID)
                .OrderByDescending( x => x.Count())
                .Take(count)
                .Select(x=>x.Key);
            var books = _context.Books.Where(x => mostLoanedBooksIds.Contains(x.Id));
            return books;
        }

        public  IEnumerable<Book> GetPageOfBooks( IEnumerable<Book> books, int pageNum = 1, int booksOnPage = 6)
        {
            pageNum--;
            if (pageNum < 0) 
                throw new ArgumentException("Invalid page number, less then 1");
            if (pageNum > books.Count() / booksOnPage + 1)
                throw new ArgumentException("Invalid page number, more then max");
            var result = books.Skip(pageNum * booksOnPage).Take(booksOnPage).ToList();
            return result;
        }

        public IEnumerable<Book> GetBooksByTag(string tag = "All")
        {
            if (tag == "All") return _context.Books;

            return _context.Books.Where(b => b.Tags.Select(t => t.Name).Contains(tag));
        }

        public IEnumerable<Tag> GetTags()
        {
            var tags = new List<Tag>() { new Tag() { Id = 0, Name = "All" } };
            tags.AddRange(_context.Tags.ToList());
            return tags;
        }

        public void AddTag(Tag newTag)
        {

            _context.Tags.Add(newTag);
            _context.SaveChanges();
            return ;
        }

        public void RemoveTag(Tag tag)
        {
            _context.Tags.Remove(tag);
            _context.SaveChanges();
            return ;
        }

        public Tag GetTag(int id)
        {
            Tag tag = _context.Tags.Find(id);
            return tag;
        }

        public void EditTag(Tag tag)
        {
            var oldTag = _context.Tags.FirstOrDefault(x=>x.Id == tag.Id);
            oldTag.Name = tag.Name;
            _context.SaveChanges();
            return;
        }

        public void Dispose()
        {
            if (_context != null) _context.Dispose();
        }
    }

    
}