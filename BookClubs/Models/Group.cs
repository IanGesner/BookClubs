using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookClubs.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string GroupPictureUrl { get; set; }
        public string GroupInfo { get; set; }
        public bool Public { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<GroupEvent> GroupEvents { get; set; }
        public virtual ICollection<GroupWallPost> GroupWallPosts { get; set; }
        public virtual ICollection<GroupInvitation> GroupInvitations { get; set; }
        public virtual ICollection<GroupRequest> GroupRequests { get; set; }

        public string OrganizerId { get; set; }
        public virtual User Organizer { get; set; }
    }
}