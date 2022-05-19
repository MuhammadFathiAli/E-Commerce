using API.Dtos;
using API.Errors;
using API.Helper;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> products(
            [FromQuery] ProductSpecParams productParams)
        {
            //var TotalItemsCount = await productService.ProductCountAsync(productParams);
            var products = await productService.GetAllProductsAsync(productParams);
            var data = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            //return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, TotalItemsCount, data));
            return Ok(data);
        }


        [HttpGet("{id}")]
        //just a sample to make swagger recognize type of responses in this action 
        //not neccassry to do it in every action 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> product(int id)
        {
            var prd = await productService.GetProductByIdAsync(id);
            if (prd == null) return BadRequest(new ApiResponse(400, "Product Doesn't Exist"));

            return mapper.Map<Product, ProductToReturnDto>(prd);

        }

       
        [HttpGet("Categories")]
        public async Task<ActionResult<List<Category>>> Categories()
        {
            return Ok(await productService.GetAllCategoriesAsync());

        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<ActionResult<ProductToReturnDto>> CreateProductAsync(ProductToReturnDto product)
        {
            var prd = await productService.GetProductByIdAsync(product.Id);
            if (prd != null)
            {
                return BadRequest(new ApiResponse(401, "This Product already exists"));
            }
            else
            {

            var ctg = await productService.GetCategoryByNameAsync(product.Category);
            var createdPrd = await productService.AddProductAsync(new Product { Category = ctg, CategoryId = ctg.Id
                , Description = product.Description, Image = product.Image.Substring(22), Price = product.Price, Quantity = 10, Title = product.Title });
            return mapper.Map<Product, ProductToReturnDto>(createdPrd);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> UpdateProductAsync(int id, ProductToReturnDto product)
        {
            if (id != product.Id) return BadRequest(new ApiResponse(400));
            var ctg = await productService.GetCategoryByNameAsync(product.Category);
            var updatedProduct = await productService.EditProduct(id,new Product {Id=product.Id, Category= ctg, Image= product.Image, CategoryId = ctg.Id,
                Description= product.Description, Price= product.Price, Quantity=10, Title = product.Title});
            if (updatedProduct == null) return BadRequest(new ApiResponse(400, "Product Couldn't be Updated"));
            return mapper.Map<Product, ProductToReturnDto>(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> ProductAsync(int id)
        {
              var prd =  await productService.DeleteProductAsync(id);
            if (prd == null) return BadRequest(new ApiResponse(400, "Product Couldn't be deleted"));
            return  mapper.Map<Product, ProductToReturnDto>(prd);

        }
        

        [HttpGet("Categories/{id}")]
        public async Task<ActionResult<Category>> Category(int id)
        {
             var category =  await productService.GetCategoryByIdAsync(id);
            if (category == null) return BadRequest(new ApiResponse(401, "Category Not Found"));
            return category;

        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost("Categories")]
        public async Task<ActionResult<Category>> CreateCategoryAsync(Category category)
        {

            var ctg = await productService.GetCategoryByIdAsync(category.Id);

            if (ctg != null) return BadRequest(new ApiResponse(401, "This Category already exists"));

            return await productService.AddCategoryAsync(new Category { Description = category.Description, Title = category.Title});
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("Categories/{id}")]
        public async Task<ActionResult<Category>> UpdateCategoryAsync(int id, Category category)
        {
            var updatedCategory = await productService.EditCategoryAsync(id, category);
            if (updatedCategory == null) return BadRequest(new ApiResponse(400, "Category Couldn't be Updated"));
            return updatedCategory;
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("Categories/{id}")]
        public async Task<ActionResult<Category>> DeleteCategoryAsync(int id)
        {
            var deletedCategory = await productService.DeleteCategoryAsync(id);
            if (deletedCategory == null) return BadRequest(new ApiResponse(400, "Product Couldn't be deleted"));
            return deletedCategory;

        }
    }
    
}
