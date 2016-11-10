using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace Base.Web.Controllers
{
    [Route("api/[controller]")]
    public class AliveController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(new {
                message = "This API is alive and watching"
            });
        }

        [HttpGet("error")]
        public async Task<IActionResult> GetErrorAsync()
        {
            throw new Exception("Test Middleware and Logging is working");
            return Ok();
        }
    }
}