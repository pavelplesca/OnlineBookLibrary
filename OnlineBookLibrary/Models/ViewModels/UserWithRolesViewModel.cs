using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace OnlineBookLibrary.Models
{
    public class UserWithRolesViewModel
    {
        public ApplicationUser User { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}