using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext context;

        public ProductRepository(StoreContext _context)
        {
           context = _context;
        }
        public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
        {
            return await context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .ToListAsync(); 
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<ProductBrand>> GetAllBrandsAsync()
        {
            return await context.ProductBrands.ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetAllTypessAsync()
        {
            return await context.ProductTypes.ToListAsync();
        }
    }
}
