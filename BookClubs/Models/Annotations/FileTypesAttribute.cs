using BookClubs.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookClubs.Models.Annotations
{
    public class FileTypesAttribute : ValidationAttribute
    {
        private readonly string[] _types;

        public FileTypesAttribute(string types)
        {
            _types = types.Replace(" ", "").Split(',', '.');
        }

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                bool isValid = false;
                var fileExtension = BcHelper.GetFileExtension((HttpPostedFileBase)value);

                foreach (var t in _types)
                {
                    if (t == fileExtension)
                        isValid = true;
                }

                return isValid;
            }
            else
                return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format("Unsupported file type. Please select one of the following types: {0}.", String.Join(", ", _types));
        }
    }
}