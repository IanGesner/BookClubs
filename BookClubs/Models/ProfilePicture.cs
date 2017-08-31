using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Web;

namespace BookClubs.Models
{
    public class ProfilePicture
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }
    }
}