using System;
using System.ComponentModel.DataAnnotations;

namespace BookClubs.Models
{
    public class GroupEvent
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public DateTime DateTime { get; set; }

        public string BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}