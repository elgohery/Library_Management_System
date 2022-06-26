using Books.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Books.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<category> categories { get; set; }
        public DbSet<student> students { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> publishers { get; set; }
        public DbSet<lending> lendings { get; set; }
    }
}
