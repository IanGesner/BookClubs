using BookClubs.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookClubs.Models.Annotations
{
    public class UniqueUserNameAttribute : ValidationAttribute
    {
        //public UniqueUserNameAttribute()
        //{
        //}

        //public override bool IsValid(object value)
        //{
        //    if (value != null)
        //    {
        //        var fileName = User.Identity.GetUserId();

        //        if (userWithSameUserName == null)
        //            return true;
        //    }

        //    return false;
        //}

        //public override string FormatErrorMessage(string name)
        //{
        //    return "This username has already been taken.";
        //}
    }
}