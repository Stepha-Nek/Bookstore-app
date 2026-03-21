using FirstAPI.Data;
using FirstAPI.Models;  
using Microsoft.AspNetCore.Authorization; //for using the Authorize attribute to secure endpoints, if needed in the future
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FirstAPI.Models
{
    [Route("api/[controller]")] //like routers
    [ApiController]
    public class BooksController : ControllerBase
    {
        //static private List<Book> books = new List<Book> //used static and not just private so changes made to the list during requests will be effected, if not static, on a new request a new list will be used, no changes
        //{

        //    new Book
        //    {
        //        ID = 1,
        //        Title = "the great gatsby",
        //        Author = "f. scott fitzgerald",
        //        YearPublished = 1925
        //    },
        //    new Book
        //    {
        //        ID = 2,
        //        Title = "to kill a mockingbird",
        //        Author = "harper lee",
        //        YearPublished = 1960
        //    },
        //    new Book
        //    {
        //        ID = 3,
        //        Title = "1984",
        //        Author = "george orwell",
        //        YearPublished = 1949
        //    },
        //    new Book
        //    {
        //        ID = 4,
        //        Title = "pride and prejudice",
        //        Author = "jane austen",
        //        YearPublished = 1813
        //    },
        //    new Book
        //    {
        //        ID = 5,
        //        Title = "moby-dick",
        //        Author = "herman melville",
        //        YearPublished = 1851
        //    }
        //};

        private readonly FirstAPIContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public BooksController(FirstAPIContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            return Ok(await _context.Books.ToListAsync());
        }

        //USING THE JSON
        //[HttpGet("{id}")]

        //public ActionResult<Book> GetBookByID(int id)
        //{
        //    var book = books.FirstOrDefault(b => b.ID == id);
        //    if (book == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(book);
        //}

        //NOW USING THE DATABASE
        [HttpGet("{id}")]

        public async Task<ActionResult<Book>> GetBookById(int id) 
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }



            //POST, WHEN USINH JSON FILE
            //[HttpPost]

            //public ActionResult<Book> AddBook(Book newBook) //newbook is the name of variable, datatype is Book
            //{
            //    if (newBook == null)
            //        return BadRequest();

            //    books.Add(newBook);
            //    return CreatedAtAction(nameof(GetBookByID), new { id = newBook.ID }, newBook);
            //}

            //NOW WHEN USING SQL SERVER DATABASE, remember when testing remove the id from the post cos the database will automatically assign an ID 
        [HttpPost]
        [Authorize] //to secure the endpoint, only authenticated users can access it to post, you can also specify roles or policies if needed

        public async Task<ActionResult<Book>> AddBook(Book newBook) //newbook is the name of variable, datatype is Book
        {
             if (newBook == null)
                 return BadRequest();

             _context.Books.Add(newBook);
             await _context.SaveChangesAsync();
             return CreatedAtAction(nameof(GetBookById), new { id = newBook.ID }, newBook);
        }

        //[HttpPut("{id}")]
        //public IActionResult UpdateBook(int id, Book UpdatedBook)
        //{
        //    var book = books.FirstOrDefault(b => b.ID == id);
        //    if (book == null)
        //    {
        //        return NotFound();
        //    }
        //    book.ID = UpdatedBook.ID;
        //    book.Title = UpdatedBook.Title;
        //    book.Author = UpdatedBook.Author;
        //    book.YearPublished = UpdatedBook.YearPublished;

        //    return NoContent();

        //}

        //NOW USING THE DATABASE for PUT
        [HttpPut("{id}")]
        [Authorize] //to secure the endpoint, only authenticated users can access it to update, you can also specify roles or policies if needed, needs jwt token in the header to access this endpoint, you can generate a token using a login endpoint and include it in the Authorization header of the request to update a book
        public async Task<IActionResult> UpdateBook(int id, Book UpdatedBook)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            book.ID = UpdatedBook.ID;
            book.Title = UpdatedBook.Title;
            book.Author = UpdatedBook.Author;
            book.YearPublished = UpdatedBook.YearPublished;

            await _context.SaveChangesAsync();

            return NoContent();

        }

        //[HttpDelete("{id}")]
        //public IActionResult DeleteBook(int id)
        //{
        //    var book = books.FirstOrDefault(b => b.ID == id);
        //    if (book == null)
        //        return NotFound();

        //    books.Remove(book);
        //    return NoContent();


        //}
        //USING DATABASE
        [HttpDelete("{id}")]
        [Authorize] //to secure the endpoint, only authenticated users can access it to delete, you can also specify roles or policies if needed. needs jwt token in the header to access this endpoint, you can generate a token using a login endpoint and include it in the Authorization header of the request to delete a book
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();//to save chnges
            return NoContent();


        }

        [HttpPost("recommend")]
        public async Task<IActionResult> RecommendBooks([FromBody] RecommendDto request)
        {
            var apiKey = _configuration["Groq__ApiKey"];
            var model = _configuration["Groq__Model"];

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var body = new
            {
                model = model,
                messages = new[]
                {
                    new
                    {
                        role = "user",
                        content = $"I just read '{request.BookTitle}'. Recommend 3 similar books in 2-3 sentences each. Be concise."
                    }
                }
            };

            var response = await client.PostAsJsonAsync("https://api.groq.com/openai/v1/chat/completions", body);
            var rawJson = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return BadRequest(rawJson);

            var result = JsonSerializer.Deserialize<JsonElement>(rawJson);
            var recommendation = result
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return Ok(new { recommendations = recommendation });
        }
    }
}
    

