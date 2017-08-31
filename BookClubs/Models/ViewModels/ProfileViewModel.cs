using BookClubs.Models.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookClubs.Models.ViewModels
{
    public class ProfileViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Tell us about you")]
        public string Biography { get; set; }
    
        [Required]
        [MaxFileSize(5, FileSizeUnit.Megabyte)]
        [FileTypes("jpg, jpeg, png, tiff, bmp")]
        public HttpPostedFileBase ProfilePicture { get; set; }

        public string ProfilePictureUrl { get; set; }
    }
}