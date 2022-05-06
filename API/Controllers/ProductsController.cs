using Core.Entities;
using Core.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository repo;
        public ProductsController(IProductRepository _repo)
        {
            repo = _repo;
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> products()
        {
            var products = await repo.GetAllProductsAsync();

          return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> product(int id)
        {
            return await repo.GetProductByIdAsync(id);

        }
        [HttpGet("Brands")]
        public async Task<ActionResult<List<ProductBrand>>> Brands(int id)
        {
            return Ok(await repo.GetAllBrandsAsync());

        }
        [HttpGet("Types")]
        public async Task<ActionResult<List<ProductType>>> Types(int id)
        {
            return Ok(await repo.GetAllTypessAsync());

        }
    }
}
