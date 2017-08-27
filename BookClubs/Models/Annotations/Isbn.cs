using System;
using System.ComponentModel.DataAnnotations;

namespace BookClubs.Models.Annotations
{
    public class Isbn : ValidationAttribute
    {
        public Isbn() : base("You must enter a valid 10 or 13 digit ISBN.") { }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string isbn = value.ToString();

                if (isbn.Length == 13 || isbn.Length == 10)                
                    return ValidationResult.Success;                
            }

            return new ValidationResult(ErrorMessage);
        }

    }
}