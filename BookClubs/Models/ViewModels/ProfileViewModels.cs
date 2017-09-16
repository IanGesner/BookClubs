using BookClubs.Models.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookClubs.Models.ViewModels
{
    public class EditProfileViewModel
    {
        [Display(Name = "Tell us about you")]
        public string Biography { get; set; }

        [Required]
        [MaxFileSize(5, FileSizeUnit.Megabyte)]
        [FileTypes("jpg, jpeg, png, tiff, bmp")]
        public HttpPostedFileBase ProfilePicture { get; set; }

        public string ProfilePictureUrl { get; set; }

        public bool Public { get; set; }
    }

    public class ProfileListViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string Biography { get; set; }
    }

    public class ProfileDetailsViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; }
        public string ProfilePictureUrl { get; set; }
        public virtual ICollection<GroupListItemViewModel> GroupsIn { get; set; }
        public virtual ICollection<ProfileListViewModel> Friends { get; set; }
        public bool PublicToViewer { get; set; }
    }
}