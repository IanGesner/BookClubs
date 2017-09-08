using System;

namespace BookClubs.Models
{
    public class FriendRequest
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime TimeStamp { get; set; }

        public string SenderId { get; set; }
        public virtual User Sender { get; set; }

        public string RecipientId { get; set; }
        public virtual User Recipient { get; set; }
    }
}