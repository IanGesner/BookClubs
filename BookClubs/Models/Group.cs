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
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<GroupEvent> GroupEvents { get; set; }
        public virtual ICollection<GroupWallPost> GroupWallPosts { get; set; }
    }
}