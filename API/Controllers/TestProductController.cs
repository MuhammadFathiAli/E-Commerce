
//using API.Dtos;
//using API.Errors;
//using API.Helper;
//using AutoMapper;
//using Core.Entities;
//using Core.Interfaces;
//using Core.Specification;
//using Microsoft.AspNetCore.Mvc;

//namespace API.Controllers
//{
//    public class ProductsssssssssssssController : BaseApiController
//    {
//        private readonly IProductService productService;
//        private readonly IMapper mapper;

//        public ProductsssssssssssssController(IProductService _productService,
//            IMapper _mapper)
//        {
//            productService = _productService;
//            mapper = _mapper;
//        }

//        [HttpGet]
//        public async Task<ActionResult<Pagination<ProductToReturnDto>>> products(
//            [FromQuery] ProductSpecParams productParams)
//        {
//            var spec = new ProductsWithTypesAndBrands(productParams);
//            var CountSpec = new ProductsWithFiltersCount(productParams);
//            var TotalItemsCount = await productRepo.CountAsync(CountSpec);
//            var products = await productRepo.ListAsync(spec);
//            var data = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

//            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, TotalItemsCount, data));
//        }









//        [HttpGet("{id}")]
//        //just a sample to make swagger recognize type of responses in this action 
//        //not neccassry to do it in every action 
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
//        public async Task<ActionResult<ProductToReturnDto>> product(int id)
//        {
//            var spec = new ProductsWithTypesAndBrands(id);
//            var prd = await productRepo.GetEntityWithSpec(spec);
//            return mapper.Map<Product, ProductToReturnDto>(prd);
//        }





//        [HttpGet("Brands")]
//        public async Task<ActionResult<List<ProductBrand>>> Brands()
//        {
//            return Ok(await productBrandRepo.GetAllAsync());

//        }


//        [HttpGet("Types")]
//        public async Task<ActionResult<List<ProductType>>> Types()
//        {
//            return Ok(await productTypeRepo.GetAllAsync());

//        }

//    }
//}