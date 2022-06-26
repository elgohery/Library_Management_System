using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models
{
    public class lending
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string bookName { get; set; }
        [Required]
        public string studentName { get; set; }
        [Required]
        public int studentid { get; set; }
        [Required]
        public DateTime landingFrom { get; set; }
        [Required]
        public DateTime landingTo { get; set; }
    }
}
