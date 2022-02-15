using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.BookStore.Models;

namespace WebClient.BookStore.Data
{
    public class BookStoreContext :IdentityDbContext<ApplicationUser>
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options) { }

        public DbSet<Books> Books { get; set; }
        public DbSet<BookGallery> BookGallery { get; set; }
        public DbSet<Language> Language { get; set; }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Books>()
                .HasOne(p => p.Language)
                .WithMany(b => Books);

        }*/

        //if we declare in startup class we don't want to use the below code
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer("Data Source = 172.16.1.192; Database = HRMS; Connect Timeout = 500; uid = demo; pwd = Demo; Max Pool Size = 500;");
            base.OnConfiguring(optionsBuilder);
        }*/
    }
}
