using FirstAPI.Models;
using FluentAssertions;

namespace firstAPI.Tests.Unit
{
    public class BookServiceTests
    {
        [Fact]
        public void Book_ShouldHaveCorrectProperties()
        {
            // Arrange
            var book = new Book
            {
                ID = 1,
                Title = "The Hobbit",
                Author = "J.R.R. Tolkien",
                YearPublished = 1937
            };

            // Act & Assert
            book.ID.Should().Be(1);
            book.Title.Should().Be("The Hobbit");
            book.Author.Should().Be("J.R.R. Tolkien");
            book.YearPublished.Should().Be(1937);
        }

        [Fact]
        public void Book_Title_ShouldNotBeEmpty()
        {
            // Arrange
            var book = new Book { Title = "" };

            // Assert
            book.Title.Should().BeEmpty();
        }
    }
}
