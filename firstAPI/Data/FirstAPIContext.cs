using Microsoft.EntityFrameworkCore;
using FirstAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FirstAPI.Data
{
    //public class FirstAPIContext: DbContext //Inheriting from DbContext class, used the one identity db context insted
    public class FirstAPIContext : IdentityDbContext<User>
    {
        public FirstAPIContext(DbContextOptions<FirstAPIContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder); // ← keeps Identity tables working


            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    ID = 1,
                    Title = "the great gatsby",
                    Author = "f. scott fitzgerald",
                    YearPublished = 1925
                },
            new Book
            {
                ID = 2,
                Title = "to kill a mockingbird",
                Author = "harper lee",
                YearPublished = 1960
            },
            new Book
            {
                ID = 3,
                Title = "1984",
                Author = "george orwell",
                YearPublished = 1949
            },
            new Book
            {
                ID = 4,
                Title = "pride and prejudice",
                Author = "jane austen",
                YearPublished = 1813
            },
            new Book
            {
                ID = 5,
                Title = "moby-dick",
                Author = "herman melville",
                YearPublished = 1851
            }

                );
        }
        public DbSet<Book> Books { get; set; }  //need dbcontext to interact with the database, like a collection of all books in the database


    }
}
