using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace BookClubs.Models
{
    public class ProfilePicture
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }

        [NotMapped]
        public Image Image { get; set; }

    }
}