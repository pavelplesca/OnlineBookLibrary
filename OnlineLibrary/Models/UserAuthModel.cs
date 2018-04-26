using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineLibrary.Models
{
    public class UserAuthModel
    {
        public UserLoginModel UserLoginModel { get; set; }
        public UserRegisterModel UserRegisterModel { get; set; }
    }
}