using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GerenciadorCampeonatos.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ErrorTestController : ControllerBase
    {
        [HttpGet("error")]
        [SwaggerOperation(Summary = "Test middleware", Description = "Test the error middleware and whether it returns the desired error.")]
        public IActionResult ThrowError()
        {
            throw new Exception("This is a test exception.");
        }
    }
}
