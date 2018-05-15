using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBookLibrary.Models.Repositories
{
    public class UserWithRolesViewModel
    {
        public ApplicationUser User { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}