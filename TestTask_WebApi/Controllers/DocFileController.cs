using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask_WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocFileController : Controller
    {
        private readonly IDocFileService repo;

        public DocFileController(IDocFileService repo)
        {
            this.repo = repo;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult GetFile(int id)
        {
            try
            {
                //string file = repo.GetFile(id);
                if (!repo.GetFile(id, out string file))
                    return NotFound();
                return Ok(file);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
