using Microsoft.AspNetCore.Mvc;

namespace StudyBot.Api.Status.Controller
{   
    [Route("[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetStatus()
        {
            return Ok();
        }
    }
}