using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgroConnect.API.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        [HttpGet] 
        public IActionResult HandleError() => Problem();
    }
}
