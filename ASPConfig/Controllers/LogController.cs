using ASPConfig.Filters;
using ASPConfig.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ASPConfig.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogController : ControllerBase
    {

        private readonly ILogger<LogController> _logger;


        internal class LogResponse
        {
            public bool isPrinted;
            public string logLevel;
        }

        public LogController(ILogger<LogController> logger)
        {
            _logger = logger;
        }

        [ServiceFilter(typeof(LogFilter))]
        [HttpGet(Name = "LogThis")]
        public JsonResult LogIt(string message, string logLevel)
        {
            logLevel = logLevel.ToLower();
            switch (logLevel) {
                case "error":
                    _logger.LogError(message);
                    break;
                case "warning":
                    _logger.LogWarning(message);
                    break;
                default:
                case "debug":
                    _logger.LogInformation(message);
                    break;
            }
            JsonResult result = new JsonResult(new {isPrinted = true, logLevel = logLevel});
            return result;
        }
    }
}