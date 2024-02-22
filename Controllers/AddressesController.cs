using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebConnectMongoDBAndSQL.Data;
using WebConnectMongoDBAndSQL.Model;

namespace WebConnectMongoDBAndSQL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressesController : ControllerBase
    {
        private readonly SQLUserDbContext _context;

        public AddressesController(SQLUserDbContext context)
        {
            _context = context;
        }

        // GET: /addresses
        [HttpGet]
        public IActionResult GetAllAddresses()
        {
            var addresses = _context.Addresses.ToList();
            return Ok(addresses);
        }

        // GET: /addresses/{id}
        [HttpGet("{id}")]
        public IActionResult GetAddressById(int id)
        {
            var address = _context.Addresses.Find(id);
            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        // POST: /addresses
        [HttpPost]
        public IActionResult CreateAddress([FromBody] Address address)
        {
            _context.Addresses.Add(address);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAddressById), new { id = address.Id }, address);
        }

        // PUT: /addresses/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateAddress(int id, [FromBody] Address updatedAddress)
        {
            var address = _context.Addresses.Find(id);
            if (address == null)
            {
                return NotFound();
            }

            address.Name = updatedAddress.Name;

            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: /addresses/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteAddress(int id)
        {
            var address = _context.Addresses.Find(id);
            if (address == null)
            {
                return NotFound();
            }

            _context.Addresses.Remove(address);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
