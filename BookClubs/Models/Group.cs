using System.Collections.Generic;

namespace BookClubs.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}