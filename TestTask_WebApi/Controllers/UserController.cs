using Infrastructure.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;

namespace TestTask_WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService repo;
        public UserController(IUserService repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<User>))]
        public IActionResult GetAllUser()
        {
            try
            {
                if (!repo.TryGetAllUser(out var allUser))
                    return NotFound();
                return Ok(allUser);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUser(int id)
        {
            try
            {
                if (!repo.TryGetUserOnId(id, out User user))
                    return NotFound();
                return Ok(user);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public HttpResponseMessage ChangeRoleUser([FromForm] int id)
        {
            try
            {
                if (!repo.ChangeRoleUser(id))
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddUser([FromForm] string userName, [FromForm] string password, [FromForm] string email, [FromForm] int kodRole)
        {
            try
            {
                User entity = new User { UserName = userName, Password = password, Email = email, KodRole = kodRole };

                if (LocalValidator(entity))
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
                repo.AddUser(entity);
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public HttpResponseMessage ChangeUser(int id, [FromForm] string newNameUser, [FromForm] string newPassword, [FromForm] string newEmail, [FromForm] string newStatus, [FromForm] int newKodRole)
        {
            try
            {
                User entity = new User { Id = id, UserName = newNameUser, Password = newPassword, Email = newEmail, Status = newStatus, KodRole = newKodRole};
                if (LocalValidator(entity))
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
                repo.ChangeUser(entity);
                return new HttpResponseMessage(HttpStatusCode.Accepted);
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public HttpResponseMessage DeleteUser(int id)
        {
            try
            {
                repo.DeleteUser(id);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult MethodForAdmin(int id, [FromForm] string command)
        {
            try
            {
                if (!repo.MethodForAdmin(id))
                    return StatusCode(StatusCodes.Status401Unauthorized);

                return Ok(command);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private bool LocalValidator(User entity)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(entity);
            if (!Validator.TryValidateObject(entity, context, results, true))
            {
                return true;
            }
            else
                return false;
        }
    }
}
