using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineLibrary.Models
{
    public enum Status
    {
        NowRenting,
        Rented,
        Violated
    }

    public class Loan
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public DateTime BorrowDate { get; set; }

        public DateTime? ReturnedDate { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public int BookID { get; set; }
        
        public Book Book { get; set; }

        #region Constructors
        public Loan() { }

        public Loan(int iD, DateTime returnDate, DateTime borrowDate, DateTime returnedDate, Status status, int bookID, Book book)
        {
            ID = iD;
            DueDate = returnDate;
            BorrowDate = borrowDate;
            ReturnedDate = returnedDate;
            Status = status;
            BookID = bookID;
            Book = book;
        }
        #endregion
    }
}