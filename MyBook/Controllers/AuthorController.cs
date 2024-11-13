using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBook.Data.Services;
using MyBook.Data.ViewModels;

namespace MyBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        public AuthorsServices _authorsServices { get; set; }

        public AuthorController(AuthorsServices authorsServices)
        {
            _authorsServices = authorsServices;
        }

        [HttpPost("add-author")]
        public IActionResult AddAuthor([FromBody] AuthorVM author)
        {
            _authorsServices.AddAuthor(author);

            return Ok();
        }

        [HttpGet("get-all-author")]
        public IActionResult GetAllAuthors()
        {
            var _allAuthor = _authorsServices.GetAllAuthors();

            return Ok(_allAuthor);
        }

        [HttpGet("get-author-by-id/{id}")]
        public IActionResult GetAuthorById(int id)
        {
            var _author = _authorsServices.GetAuthorById(id);

            return Ok(_author);
        }

        [HttpPut("update-author-by-id/{id}")]
        public IActionResult UpdateAuthorById(int id, [FromBody] AuthorVM author)
        {
            var _author = _authorsServices.UpdateAuthorById(id, author);

            return Ok(_author);
        }

        [HttpDelete("delete-author-by-id/{id}")]
        public IActionResult DeleteAuthorById(int id)
        {
            _authorsServices.DeleteAuthorById(id);

            return Ok();
        }
    }
}
