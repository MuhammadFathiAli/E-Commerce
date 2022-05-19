using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductService(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public async Task<IReadOnlyList<Product>> GetAllProductsAsync(ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrands(productParams);
            var CountSpec = new ProductsWithFiltersCount(productParams);
            //var TotalItemsCount = await unitOfWork.Repository<Product>().CountAsync(CountSpec);
            var products = await unitOfWork.Repository<Product>().ListAsync(spec);
            return products;
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            var spec = new ProductsWithTypesAndBrands(id);
            return await unitOfWork.Repository<Product>().GetEntityWithSpec(spec);

        }
        public async Task<IReadOnlyList<Category>> GetAllCategoriesAsync()
        {
            return await unitOfWork.Repository<Category>().GetAllAsync();
        }
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
               var category = await unitOfWork.Repository<Category>().GetByIdAsync(id);
            if (category == null) return null;
            return category;

        }
        public async Task<Category> GetCategoryByNameAsync(string ctg)
        {
            var spec = new CategoryWithNameSpecification(ctg);
            return await unitOfWork.Repository<Category>().GetEntityWithSpec(spec);

        }
        public async Task<Category> AddCategoryAsync(Category category)
        {
            unitOfWork.Repository<Category>().Add(category);
            var results = await unitOfWork.Complete();
            if (results <= 0) return null;
            return category;
        }

        public async Task<Product> AddProductAsync(Product product)
        {

            unitOfWork.Repository<Product>().Add(product);
            var results = await unitOfWork.Complete();
            if (results <= 0) return null;
            return product;
        }


        public async Task<Category> DeleteCategoryAsync(int id)
        {
            var category = await unitOfWork.Repository<Category>().GetByIdAsync(id);
            unitOfWork.Repository<Category>().Delete(category);
            var results = await unitOfWork.Complete();
            if (results <= 0) return null;
            return category;
        }

        public  async Task<Product> DeleteProductAsync(int id)
        {
            var product = await unitOfWork.Repository<Product>().GetByIdAsync(id);
            if (product == null) return null;
            unitOfWork.Repository<Product>().Delete(product);
            var results = await unitOfWork.Complete();
            if (results <= 0) return null;
            return product;
        }

        public async Task<Category> EditCategoryAsync(int id, Category category)
        {
            unitOfWork.Repository<Category>().Update(category);
            var result = await unitOfWork.Complete();

            if (result <= 0) return null;
            return category;
        }

        public async Task<Product> EditProduct(int id, Product product)
        {
            unitOfWork.Repository<Product>().Update(product);
            var result = await unitOfWork.Complete();

            if (result <= 0) return null;
            return product;
        }

        public async Task<int> ProductCountAsync(ProductSpecParams productParams)
        {
            var CountSpec = new ProductsWithFiltersCount(productParams);
            return await unitOfWork.Repository<Product>().CountAsync(CountSpec);
        }
    }
}
