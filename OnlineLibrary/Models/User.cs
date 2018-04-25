using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OnlineLibrary.Models
{
    public class User: IdentityUser
    {
        public DateTime? BannedUntil { get; set; }
        public bool IsBanned { get; set; }
        public int ViolationsNr { get; set; }

        public User()
        {

        }
        
    }
}