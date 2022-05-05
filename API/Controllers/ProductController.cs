using Core.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly StoreContext _context;
        public ProductController(StoreContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult<List<Product>> products()
        {
            List<Product> products = _context.Products.ToList();

          return products;
        }
        [HttpGet("{id}")]
        public ActionResult<Product> product(int id)
        {
            Product prd = _context.Products.Find(id);

            return prd;
        }
    }
}
