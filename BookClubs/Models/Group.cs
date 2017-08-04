using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookClubs.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<GroupEvent> GroupEvents { get; set; }
    }
}