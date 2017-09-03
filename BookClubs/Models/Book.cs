using BookClubs.Models.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookClubs.Models
{
    public class Book
    {
        [Key]
        //[Isbn]
        public string Isbn { get; set; }
        //public string Isbn
        //{
        //    get { return Isbn; }
        //    set
        //    {
        //        string[] isbnParts = value.ToString().Split(' ', '-');
        //        string isbn = String.Join("", isbnParts);

        //        value = isbn;
        //    }
        //}

        [Required]
        public string Title { get; set; }
        public ICollection<Author> Authors { get; set; }
    }
}