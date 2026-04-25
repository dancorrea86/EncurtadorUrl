using EncurtadorUrl.Api.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EncurtadorUrl.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncurtadorController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Encurtador de URL API is running.");
        }

        [HttpPost]
        public IActionResult Post([FromBody] UrlDTO url)
        {
            int hashCode = url.GetHashCode();

            return Ok(hashCode);
        }
    }
}
