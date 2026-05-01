using EncurtadorUrl.Api.DTO;
using EncurtadorUrl.Data.Context;
using EncurtadorUrl.Service.Encurtador;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EncurtadorUrl.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncurtadorController : ControllerBase
    {
        private readonly IUrlService _service;

        public EncurtadorController(IUrlService context)
        {
            this._service = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Encurtador de URL API is running.");
        }

        [HttpPost]
        public IActionResult Post([FromBody] UrlDTO url)
        {
            if (!Uri.IsWellFormedUriString(url.Url, UriKind.Absolute))
            { 
                return BadRequest("Por favor, insira uma URL válida.");
            };

            string esquema = Request.Scheme;
            string host = Request.Host.ToString();     

            string hashCode = _service.EncurtarUrl(url.Url, esquema, host);

            UrlDTO urlEncurtada = new UrlDTO { Url = hashCode };

            return CreatedAtAction(nameof(Post), urlEncurtada);
        }
    }
}
