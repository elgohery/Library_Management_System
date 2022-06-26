using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models
{
    public class Publisher
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Publisher Name")]
        public string name { get; set; }
        [Required]
        [Display(Name = "Publisher Phone")]
        public string phonenum { get; set; }

        public string address { get; set; }
        [Required]
        [Display(Name = "Publisher Email")]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        [Display(Name = "Book Name")]
        
        public string bookName { get; set; }
    }
}
