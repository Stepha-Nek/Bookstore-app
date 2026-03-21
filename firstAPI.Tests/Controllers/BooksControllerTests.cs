using firstAPI.Tests.Helpers;
using FluentAssertions;
using System.Net;

namespace firstAPI.Tests.Controllers
{
    public class BooksControllerTests : IClassFixture<TestWebAppFactory>
    {
        private readonly HttpClient _client;

        public BooksControllerTests(TestWebAppFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetBooks_ShouldReturn200()
        {
            // Act
            var response = await _client.GetAsync("/api/books");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task AddBook_WithoutToken_ShouldReturn401()
        {
            // Arrange
            var book = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(new
                {
                    title = "Test Book",
                    author = "Test Author",
                    yearPublished = 2024
                }),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            // Act
            var response = await _client.PostAsync("/api/books", book);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }

}