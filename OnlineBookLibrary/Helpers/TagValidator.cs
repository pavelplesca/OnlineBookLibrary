using OnlineBookLibrary.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineBookLibrary.Helpers
{
    public class TagValidator: ValidationAttribute
    {
        private readonly int minElements;
        public TagValidator(int minElements)
        {
            this.minElements = minElements;
        }

        public override bool IsValid(object tags)
        {
            var list = tags as ICollection;
            if (list != null)
            {
                return list.Count >= minElements;
            }
            return false;
        }
    }
}