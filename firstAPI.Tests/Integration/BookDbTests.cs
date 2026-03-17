using FirstAPI.Data;
using FirstAPI.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace firstAPI.Tests.Integration
{
    public class BookDbTests
    {
        private FirstAPIContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<FirstAPIContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new FirstAPIContext(options);
            context.Database.EnsureCreated();
            return context;
        }
        [Fact]
        public async Task AddBook_ShouldSaveToDatabase()
        {
            var context = GetInMemoryContext();
            var book = new Book { Title = "1984", Author = "George Orwell", YearPublished = 1949 };

            context.Books.Add(book);
            await context.SaveChangesAsync();

            var saved = context.Books.FirstOrDefault(b => b.Title == "1984");
            saved.Should().NotBeNull();
            saved!.Title.Should().Be("1984");
        }
        [Fact]
        public async Task DeleteBook_ShouldRemoveFromDatabase()
        {
            var context = GetInMemoryContext();
            var book = new Book { Title = "UniqueTestBook123", Author = "Test Author", YearPublished = 2024 };
            context.Books.Add(book);
            await context.SaveChangesAsync();

            var toDelete = context.Books.First(b => b.Title == "UniqueTestBook123");
            context.Books.Remove(toDelete);
            await context.SaveChangesAsync();

            var deleted = context.Books.FirstOrDefault(b => b.Title == "UniqueTestBook123");
            deleted.Should().BeNull();
        }

    }
}
