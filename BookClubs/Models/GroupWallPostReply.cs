using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookClubs.Models
{
    public class GroupWallPostReply
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime TimeStamp { get; set; }

        public int OriginalPostId { get; set; }
        public virtual GroupWallPost OriginalPost { get; set; }

        public string PosterId { get; set; }
        public virtual User Poster { get; set; }
    }
}