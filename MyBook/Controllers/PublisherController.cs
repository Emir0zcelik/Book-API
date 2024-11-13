using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBook.Data.Services;
using MyBook.Data.ViewModels;

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
            _publishersServices.AddPublisher(publisher);

            return Ok();
        }

        [HttpGet("get-all-publisher")]
        public IActionResult GetAllPublisher()
        {
            var _allPublisher = _publishersServices.GetAllPublishers();

            return Ok(_allPublisher);
        }

        [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetPublisherById(int id)
        {
            var _publisher = _publishersServices.GetPublisherById(id);

            return Ok(_publisher);
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
    }
}
