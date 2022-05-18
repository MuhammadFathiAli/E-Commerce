using Core.Entities;
using Core.Specification;

namespace Core.Interfaces
{
    public interface IProductService
    {
        public Task<IReadOnlyList<Product>> GetAllProductsAsync(ProductSpecParams parms);
        public Task<Product> GetProductByIdAsync(int id);
        public Task<IReadOnlyList<Category>> GetAllCategoriesAsync();
        public Task<Category> GetCategoryByIdAsync(int id);
        public Task<Product> EditProduct(int id, Product product);
        public Task<Product> DeleteProductAsync(int id);
        public Task<Product> AddProductAsync (Product product);
        public Task<Category> EditCategoryAsync(int id, Category category);
        public Task<Category> DeleteCategoryAsync(int id);
        public Task<Category> AddCategoryAsync (Category category);
        public Task<int> ProductCountAsync(ProductSpecParams productParams);
        public Task<Category> GetCategoryByNameAsync(string ctg);


    }
}
