﻿using Core.Entities;
using Core.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository repo;
        public ProductController(IProductRepository _repo)
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
    }
}
