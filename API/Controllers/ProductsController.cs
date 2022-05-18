using API.Dtos;
using API.Errors;
using API.Helper;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public ProductsController(IProductService _productService,
            IMapper _mapper)
        {
            productService = _productService;
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> products(
            [FromQuery] ProductSpecParams productParams)
        {
            var TotalItemsCount = await productService.ProductCountAsync(productParams);
            var products = await productService.GetAllProductsAsync(productParams);
            var data = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, TotalItemsCount, data));
        }



        [HttpGet("{id}")]
        //just a sample to make swagger recognize type of responses in this action 
        //not neccassry to do it in every action 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> product(int id)
        {
            var prd = await productService.GetProductByIdAsync(id);
            //return mapper.Map<Product, ProductToReturnDto>(prd);
            return prd;
        }


        [HttpGet("Brands")]
        public async Task<ActionResult<List<ProductBrand>>> Brands()
        {
            return Ok(await productService.GetAllProductBrandsAsync());

        }
        [HttpGet("Brands/{id}")]

        public async Task<ActionResult<ProductBrand>> Brand(int id)
        {
            return await productService.GetProductBrandByIdAsync(id);
        }


        [HttpGet("Types")]
        public async Task<ActionResult<List<ProductType>>> Types()
        {
            return Ok(await productService.GetAllProductTypesAsync());

        }


        [HttpPost]
        public async Task<ActionResult<Product>> CreateProductAsync(ProductToReturnDto product)
        {
            var InputPrd = mapper.Map<ProductToReturnDto, Product>(product);
            var prd = await productService.AddProductAsync(InputPrd);
            return prd;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> UpdateProductAsync(int id, Product product)
        {
            var prd = await productService.EditProduct(id,product);
            return mapper.Map<Product, ProductToReturnDto>(prd);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> ProductAsync(int id)
        {
           return await productService.DeleteProductAsync(id);
            
        }


        //            public async Task<ActionResult<Order>> CreateOrderAsync(OrderDto orderDto)
        //{
        //    var email = HttpContext.User.RetriveEmailFromPrincipal();
        //    var address = mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);
        //    var order = await orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);
        //    if (order == null) return BadRequest(new ApiResponse(400, "Problem Creating Order"));
        //    return Ok(order);
        //}

    }
}
