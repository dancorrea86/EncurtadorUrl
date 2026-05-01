using EncurtadorUrl.Service.Encurtador;
using Microsoft.AspNetCore.Mvc;

namespace EncurtadorUrl.Api.Controllers
{
    public class RedirectController : Controller
    {
        private readonly IUrlService _service;

        public RedirectController(IUrlService context)
        {
            this._service = context;
        }

        [HttpGet("{hash}")]
        public IActionResult Get(string hash)
        {
            string urlOriginal = _service.Redirect(hash);

            if (urlOriginal != null)
            {
                return Redirect(urlOriginal);
            }
            return NotFound();
        }
    }
}
