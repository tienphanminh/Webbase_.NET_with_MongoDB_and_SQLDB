using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebConnectMongoDBAndSQL.Data;
using WebConnectMongoDBAndSQL.Model;

namespace WebConnectMongoDBAndSQL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly SQLUserDbContext _context;

        public UsersController(SQLUserDbContext context)
        {
            _context = context;
        }

        // GET: /users
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        // GET: /users/{id}
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            // Store UserId in cookie
            Response.Cookies.Append("UserId", id.ToString(), new CookieOptions
            {
                Expires = DateTime.Now.AddHours(1) // Example: Cookie expires in 1 hour
            });

            return Ok(user);
        }

        // POST: /users
        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        // PUT: /users/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Username = updatedUser.Username;
            user.Password = updatedUser.Password;

            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: /users/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
