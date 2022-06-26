using Books.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.ViewModel
{
    public class BookViewModel
    {
        public Book book { get; set; }
        public List<Book> books { get; set; }
        public List<category> categories { get; set; }
        public IFormFile image { get; set; }

    }
}
