using Core.Entities;
using Core.Specification;

namespace Core.Interfaces
{
    public interface IProductService
    {
        public Task<IReadOnlyList<Product>> GetAllProductsAsync(ProductSpecParams parms);
        public Task<Product> GetProductByIdAsync(int id);
        public Task<IReadOnlyList<ProductBrand>> GetAllProductBrandsAsync();
        public Task<ProductBrand> GetProductBrandByIdAsync(int id);
        public Task<IReadOnlyList<ProductType>> GetAllProductTypesAsync();
        public Task<Product> EditProduct(int id, Product product);
        public Task<Product> DeleteProductAsync(int id);
        public Task<Product> AddProductAsync (Product product);
        public Task<ProductBrand> EditBrandAsync(int id, ProductBrand brand);
        public Task<ProductBrand> DeleteBrandAsync(int id);
        public Task<ProductBrand> AddBrandAsync (ProductBrand brand);
        public Task<int> ProductCountAsync(ProductSpecParams productParams);


    }
}
