using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineLibrary.Models
{
    public class TestUser
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<Loan> Loans { get; set; }

        public TestUser()
        {

        }

        public TestUser(int iD, string name, ICollection<Loan> loans)
        {
            ID = iD;
            Name = name;
            Loans = loans;
        }
    }
}