using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineLibrary.Models
{
    public enum Status
    {
        Active,
        Canceled,
        Violated
    }

    public class Loan
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }

        [Required]
        public DateTime BorrowDate { get; set; }

        [Required]
        public DateTime ReturnedDate { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public int BookID { get; set; }
        
        [Required]
        public Book Book { get; set; }
    }
}