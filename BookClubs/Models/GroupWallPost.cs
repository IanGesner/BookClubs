using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookClubs.Models
{
    public class GroupWallPost
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime TimeStamp { get; set; }

        public string PosterId { get; set; }
        public virtual User Poster { get; set; }

        public int GroupId { get; set; }
        public virtual Group Group { get; set; }

        public virtual ICollection<GroupWallPostReply> Replies { get; set; }
    }
}