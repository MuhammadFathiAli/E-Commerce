﻿using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
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
        private readonly IGenericRepository<Product> productRepo;
        private readonly IGenericRepository<ProductBrand> productBrandRepo;
        private readonly IGenericRepository<ProductType> productTypeRepo;
        private readonly IMapper mapper;

        public ProductsController(IGenericRepository<Product> _productRepo,
            IGenericRepository<ProductBrand> _productBrandRepo,
            IGenericRepository<ProductType> _productTypeRepo,
            IMapper _mapper)
        {
            this.productRepo = _productRepo;
            this.productBrandRepo = _productBrandRepo;
            this.productTypeRepo = _productTypeRepo;
            this.mapper = _mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<ProductToReturnDto>>> products()
        {
            var spec = new ProductsWithTypesAndBrands();
            var products = await productRepo.ListAsync(spec);

          return Ok(mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> product(int id)
        { 
            var spec = new ProductsWithTypesAndBrands(id);
            var prd = await productRepo.GetEntityWithSpec(spec);
            return mapper.Map<Product,ProductToReturnDto>(prd);

        }
        [HttpGet("Brands")]
        public async Task<ActionResult<List<ProductBrand>>> Brands(int id)
        {
            return Ok(await productBrandRepo.GetAllAsync());

        }
        [HttpGet("Types")]
        public async Task<ActionResult<List<ProductType>>> Types(int id)
        {
            return Ok(await productTypeRepo.GetAllAsync());

        }
    }
}