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
        [Display(Name = "Current Book")]
        public string CurrentBookTitle { get; set; }
        [Display(Name = "Members")]
        public string MemberCount { get; set; }

        //public Group Group { get; set; }
        //public GroupEvent NextEvent { get; set; }
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

        //public Group Group { get; set; }
        //public GroupEvent NextEvent { get; set; }
    }
}