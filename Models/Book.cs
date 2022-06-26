using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models
{
    public class Book
    {
        [Key]
        public int bookId { get; set; }
        [Required(ErrorMessage ="يجب اختيار اسم")]
        [MaxLength(40)]
        [MinLength(3)]
        public string bookName { get; set; }
        [Required]
        [MaxLength(40)]
        [MinLength(3)]
        public string AuthorName { get; set; }
        public string imgPath { get; set; }
        public string desc { get; set; }
        [Required]
        [ForeignKey("category")]
        [Range(1,10000,ErrorMessage ="يجب اختيار قسم")]
        public int categoryId { get; set; }
        
        public category category { get; set; }
    }
}
