using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyBook.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiVersion("1.2")]
    [ApiVersion("1.9")]
    [Route("api/[controller]")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("get-test-data")]
        public IActionResult Getv1() 
        {
            return Ok("This is test controller v1.0");
        }

        [HttpGet("get-test-data"), MapToApiVersion("1.2")]
        public IActionResult Getv12()
        {
            return Ok("This is test controller v1.2");
        }

        [HttpGet("get-test-data"), MapToApiVersion("1.9")]
        public IActionResult Getv19()
        {
            return Ok("This is test controller v1.9");
        }
    }
}
