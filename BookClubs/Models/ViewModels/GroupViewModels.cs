using BookClubs.Models.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookClubs.Models.ViewModels
{
    public class GroupListItemViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Group")]
        public string GroupName { get; set; }

        [Display(Name = "State")]
        public string GroupState { get; set; }

        [Display(Name = "City")]
        public string GroupCity { get; set; }

        [Display(Name = "Members")]
        public string MemberCount { get; set; }
    }

    public class GroupDetailsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Group")]
        public string GroupName { get; set; }

        [Display(Name = "State")]
        public string GroupState { get; set; }

        [Display(Name = "City")]
        public string GroupCity { get; set; }

        [Display(Name = "Current Book")]
        public string CurrentBookTitle { get; set; }

        [Display(Name = "Members")]
        public string MemberCount { get; set; }

        public string ProfilePictureUrl { get; set; }

        public ICollection<ProfileListViewModel> MemberProfiles { get; set; }

        public ICollection<GroupWallPostListViewModel> WallPosts { get; set; }

        public ICollection<GroupEventListViewModel> GroupEvents { get; set; }

        public bool IsOrganizer { get; set; }

        public bool IsMember { get; set; }

        public bool IsPublic { get; set; }

        public string CurrentUserId { get; set; }
    }

    public class GroupCreateViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [MaxFileSize(5, FileSizeUnit.Megabyte)]
        [FileTypes("jpg, jpeg, png, tiff, bmp")]
        [Display(Name = "Group Picture")]
        public HttpPostedFileBase GroupPicture { get; set; }

        public string GroupPictureUrl { get; set; }

        [Display(Name = "Group Information")]
        public string GroupInfo { get; set; }

        public bool Public { get; set; }
    }

    public class GroupEventListViewModel
    {
        public int Id { get; set; }
        public string DateTime { get; set; }
        public string Location { get; set; }
        public string BookName { get; set; }
    }

    public class GroupWallPostListViewModel
    {
        public int Id { get; set; }
        public string PosterId { get; set; }
        public string PosterName { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string DateTime { get; set; }
        public string Body { get; set; }
        public ICollection<GroupWallPostReplyViewModel> Replies { get; set; }
    }

    public class GroupWallPostReplyViewModel
    {
        public int Id { get; set; }
        public string PosterId { get; set; }
        public string PosterName { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string DateTime { get; set; }
        public string Body { get; set; }
    }
}