using System;
using System.ComponentModel.DataAnnotations;

namespace BookClubs.Models
{
    public class GroupEvent
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string ZipCode { get; set; }

        [Required]
        public virtual Book Book { get; set; }

        [Required]
        public DateTime DateTime { get; set; }
    }
}