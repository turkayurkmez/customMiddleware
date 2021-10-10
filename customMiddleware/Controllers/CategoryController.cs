using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace customMiddleware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpPost]
        public IActionResult AddCategory()
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetCategory()
        {
            return Ok("Bir kategori...");
        }
    }
}
