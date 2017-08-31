using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookClubs.Models.Annotations
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _fileSize;
        private readonly FileSizeUnit _sizeUnit;

        public MaxFileSizeAttribute(int fileSize, FileSizeUnit sizeUnit)
        {
            _fileSize = fileSize;
            _sizeUnit = sizeUnit;
        }

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                if (((HttpPostedFileBase)value).ContentLength / (int)_sizeUnit > _fileSize)
                    return false;
            }

            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format("File size must be less than {0} {1}{2}.",
                                                                _fileSize,
                                                                _sizeUnit.ToString().ToLower(),
                                                                _fileSize > 1 ? "s" : "");
        }
    }

    public enum FileSizeUnit { Byte = 1, Kilobyte = 1024, Megabyte = 1048576 };
}