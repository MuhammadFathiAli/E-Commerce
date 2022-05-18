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
            var TotalItemsCount = await unitOfWork.Repository<Product>().CountAsync(CountSpec);
            var products = await unitOfWork.Repository<Product>().ListAsync(spec);
            return products;
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            var spec = new ProductsWithTypesAndBrands(id);
            return await unitOfWork.Repository<Product>().GetEntityWithSpec(spec);

        }
        public async Task<IReadOnlyList<ProductBrand>> GetAllProductBrandsAsync()
        {
            return await unitOfWork.Repository<ProductBrand>().GetAllAsync();
        }
        public async Task<ProductBrand> GetProductBrandByIdAsync(int id)
        {
            return await unitOfWork.Repository<ProductBrand>().GetByIdAsync(id);

        }
        public async Task<IReadOnlyList<ProductType>> GetAllProductTypesAsync()
        {
            return await unitOfWork.Repository<ProductType>().GetAllAsync();
        }

        public async Task<ProductBrand> AddBrandAsync(ProductBrand brand)
        {
            unitOfWork.Repository<ProductBrand>().Add(brand);
            var results = await unitOfWork.Complete();
            if (results <= 0) return null;
            return brand;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            unitOfWork.Repository<Product>().Add(product);
            var results = await unitOfWork.Complete();
            if (results <= 0) return null;
            return product;
        }


        public async Task<ProductBrand> DeleteBrandAsync(int id)
        {
            var brand = await unitOfWork.Repository<ProductBrand>().GetByIdAsync(id);
            unitOfWork.Repository<ProductBrand>().Delete(brand);
            var results = await unitOfWork.Complete();
            if (results <= 0) return null;
            return brand;
        }

        public  async Task<Product> DeleteProductAsync(int id)
        {
            var product = await unitOfWork.Repository<Product>().GetByIdAsync(id);
            unitOfWork.Repository<Product>().Delete(product);
            var results = await unitOfWork.Complete();
            if (results <= 0) return null;
            return product;
        }

        public async Task<ProductBrand> EditBrandAsync(int id, ProductBrand brand)
        {
            var updatedbrand = await unitOfWork.Repository<ProductBrand>().GetByIdAsync(id);
            unitOfWork.Repository<ProductBrand>().Update(updatedbrand);
            var results = await unitOfWork.Complete();
            if (results <= 0) return null;
            return updatedbrand;
        }

        public async Task<Product> EditProduct(int id, Product product)
        {
            var updatedproduct = await unitOfWork.Repository<Product>().GetByIdAsync(id);
            unitOfWork.Repository<Product>().Update(updatedproduct);
            var results = await unitOfWork.Complete();
            if (results <= 0) return null;
            return updatedproduct;
        }

        public async Task<int> ProductCountAsync(ProductSpecParams productParams)
        {
            var CountSpec = new ProductsWithFiltersCount(productParams);
            return await unitOfWork.Repository<Product>().CountAsync(CountSpec);
        }
    }
}
