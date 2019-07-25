using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Ocelot.CustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Customer Controller" };
        }
    }
}