using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models
{
    public class category
    {
        [Key]
        public int categoryId { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string name { get; set; }
    }
}
