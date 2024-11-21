using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBook.Data.Services;
using MyBook.Data.ViewModels;
using System;

namespace MyBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        public PublishersServices _publishersServices { get; set; }

        public PublisherController(PublishersServices publishersServices)
        {
            _publishersServices = publishersServices;
        }

        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            var _newPublisher = _publishersServices.AddPublisher(publisher);

            return Created(nameof(AddPublisher), _newPublisher);
        }

        [HttpGet("get-all-publisher")]
        public IActionResult GetAllPublisher(string sortBy, string searchString)
        {
            try
            {
                var _allPublisher = _publishersServices.GetAllPublishers(sortBy, searchString);

                return Ok(_allPublisher);
            }
            catch (Exception)
            {

                return BadRequest("Sorry, we could not load the publishers");
            }

        }

        [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetPublisherById(int id)
        {
            var _publisher = _publishersServices.GetPublisherById(id);

            if (_publisher != null)
            {
                return Ok(_publisher);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("update-publisher-by-id/{id}")]
        public IActionResult UpdatePublisherById(int id, [FromBody] PublisherVM publisher)
        {
            var _publisher = _publishersServices.UpdatePublisherById(id, publisher);

            return Ok(_publisher);
        }

        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            _publishersServices.DeletePublisherById(id);

            return Ok();
        }

        [HttpGet("get-publisher-books-with-authors/{id}")]
        public IActionResult GetPublisherBooksWithAuthors(int id)
        {
            try
            {
                int x1 = 1;
                int x2 = 0;
                int result = x1 / x2;

                var _publisherData = _publishersServices.GetPublisherData(id);

                return Ok(_publisherData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
