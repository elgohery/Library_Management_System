using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Author Name")]
        public string name { get; set; }
        [Required]
        [Display(Name = "Author Phone")]
        public string phonenum { get; set; }

        public string address { get; set; }
        [Required]
        [Display(Name = "Author Email")]
        [EmailAddress]
        public string email { get; set; }
    }
}
