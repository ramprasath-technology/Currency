using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Currency.Controllers.HealthCheck
{
    [ApiVersion("1.0")]
    [Route("v{v:apiVersion}/HealthCheck")]
    [ApiController]
    public class HealthCheckV1Controller : ControllerBase
    {
        [HttpGet]
        public ActionResult GetHealthCheck()
        {
            return Ok("The app is running");
        }
    }
}
