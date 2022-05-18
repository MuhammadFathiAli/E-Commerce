//using Core.Entities;
//using Core.Interfaces;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Infrastructure.Data
//{
//    public class ProductRepository : IProductRepository
//    {
//        private readonly StoreContext context;

//        public ProductRepository(StoreContext _context)
//        {
//           context = _context;
//        }
//        public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
//        {
//            return await context.Products
//                .Include(p => p.ProductType)
//                .Include(p => p.ProductBrand)
//                .ToListAsync(); 
//        }

//        public async Task<Product> GetProductByIdAsync(int id)
//        {
//            return await context.Products
//                .Include(p => p.ProductType)
//                .Include(p => p.ProductBrand)
//                .FirstOrDefaultAsync(p => p.Id == id);
//        }

//        public async Task<IReadOnlyList<Category>> GetAllBrandsAsync()
//        {
//            return await context.ProductBrands.ToListAsync();
//        }

//        public async Task<IReadOnlyList<ProductType>> GetAllTypesAsync()
//        {
//            return await context.ProductTypes.ToListAsync();
//        }


//        ////updating => replace the exisitng basket with the basket coming from client 
//        //public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
//        //{
//        //    var created = await database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(7));
//        //    if (!created) return null;
//        //    return await GetBasketAsync(basket.Id);
//        //}

//        public Task<Product> UpdateProductAsync(Product product)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> DeleteProductAsync(int id)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
