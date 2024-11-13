using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBook.Data.Services;
using MyBook.Data.ViewModels;

namespace MyBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public BooksServices _booksServices { get; set; }

        public BooksController(BooksServices booksServices)
        {
            _booksServices = booksServices;
        }

        [HttpPost("add-book")]
        public IActionResult AddBook([FromBody]BookVM book)
        {
            _booksServices.AddBook(book);

            return Ok();
        }

        [HttpGet("get-all-books")]
        public IActionResult GetAllBooks() 
        {
            var _allBooks = _booksServices.GetAllBooks();

            return Ok(_allBooks);
        }

        [HttpGet("get-book-by-id/{id}")]
        public IActionResult GetBookById(int id) 
        {
            var _book = _booksServices.GetBookById(id);

            return Ok(_book);
        }

        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateBookById(int id, [FromBody]BookVM book)
        {
            var _book = _booksServices.UpdateBookById(id, book);

            return Ok(_book);
        }

        [HttpDelete("delete-book-by-id/{id}")]
        public IActionResult DeleteBookById(int id) 
        {
            _booksServices.DeleteBookById(id);

            return Ok();
        }
    }
}
