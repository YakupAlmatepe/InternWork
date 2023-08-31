
using Microsoft.AspNetCore.Mvc;

namespace Intern.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogger<LogController> _logger;

        public LogController(ILogger<LogController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into LogController");
        }

        [HttpGet("[action]")]
        public IActionResult Index()
        {
            _logger.LogInformation("Hello, this is the index!");
            return NoContent();
        }

    }
}
