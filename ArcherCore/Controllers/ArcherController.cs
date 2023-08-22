using ArcherCore.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ArcherCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArcherController : ControllerBase
    {
        /// <summary>
        /// Check Status of API
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Archer", "API V1.0" };
        }

        [HttpGet("GetLogsFromText")]
        public async Task<string> GetLogsFromText()
        {
            var logs = await LoggingUtilities.GetLogFromText();

            if(logs != null)
            {
                return JsonConvert.SerializeObject(new { Success = true, Logs = logs }, Formatting.Indented);
            }
            else
            {
                return JsonConvert.SerializeObject(new { Success = false, Logs = "" }, Formatting.Indented);
            }
        }
    }
}
