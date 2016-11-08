using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
﻿using System.Threading.Tasks;

namespace Base.Web.Controllers
{
    [Route("api/[controller]")]
    public class AliveController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(new {
                message = "This API is alive"
            });
        }
    }
}