using Amazon.Runtime.Internal;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using WebConnectMongoDBAndSQL.Data;
using WebConnectMongoDBAndSQL.Model;

namespace WebConnectMongoDBAndSQL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMongoCollection<Order> _orders;

        public OrdersController(MongoDbContext dbContext)
        {
            _orders = dbContext.Orders;
        }

        [HttpGet]
        public ActionResult<List<Order>> Get() =>
            _orders.Find(order => true).ToList();

        [HttpPost]
        public ActionResult<Order> Create(Order order)
        {
            if (Request.Cookies.TryGetValue("UserId", out string userId))
            {
                order.UserId = userId;
                _orders.InsertOne(order);
                return order;
            }
            else
            {
                return BadRequest("UserId not found in cookie.");
            }
        }

        [HttpGet("{id:length(24)}", Name = "GetOrder")]
        public ActionResult<Order> Get(string id)
        {
            var order = _orders.Find<Order>(o => o.Id == id).FirstOrDefault();
            if (order == null)
            {
                return NotFound();
            }
            return order;
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Order updatedOrder)
        {
            var order = _orders.Find<Order>(o => o.Id == id).FirstOrDefault();
            if (order == null)
            {
                return NotFound();
            }
            _orders.ReplaceOne(o => o.Id == id, updatedOrder);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var order = _orders.Find<Order>(o => o.Id == id).FirstOrDefault();
            if (order == null)
            {
                return NotFound();
            }
            _orders.DeleteOne(o => o.Id == id);
            return NoContent();
        }

        

    }
}
