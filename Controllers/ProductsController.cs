using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Xml.Linq;
using WebConnectMongoDBAndSQL.Data;
using WebConnectMongoDBAndSQL.Model;

namespace WebConnectMongoDBAndSQL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMongoCollection<Product> _products;

        public ProductsController(MongoDbContext dbContext)
        {
            _products = dbContext.Products;
        }

        [HttpGet]
        public ActionResult<List<Product>> Get() =>
            _products.Find(product => true).ToList();

        [HttpPost]
        public ActionResult<Product> Create(Product product)
        {
            _products.InsertOne(product);
            return product;
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        public ActionResult<Product> Get(string id)
        {
            var product = _products.Find<Product>(p => p.Id == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Product updatedProduct)
        {
            var product = _products.Find<Product>(p => p.Id == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }
            _products.ReplaceOne(p => p.Id == id, updatedProduct);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var product = _products.Find<Product>(p => p.Id == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }
            _products.DeleteOne(p => p.Id == id);
            return NoContent();
        }
    }
}
