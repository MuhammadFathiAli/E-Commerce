using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class ProductsWithTypesAndBrands : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrands(ProductSpecParams productParams) 
            : base (x =>
            (string.IsNullOrEmpty(productParams.Search) || x.Title.ToLower().Contains(productParams.Search)) &&
            (!productParams.CategoryId.HasValue || x.CategoryId == productParams.CategoryId)
            )
        {
            AddInclude(x => x.Category);
            AddOrderBy(x => x.Title);
            ApplyPaging(productParams.PageSize, productParams.PageSize*(productParams.PageIndex - 1));

            if (!string.IsNullOrEmpty(productParams.sort))
            {
                switch (productParams.sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescinding(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Title);
                        break;
                }
            }
        }
        public ProductsWithTypesAndBrands(int id) : base(x=> x.Id == id)
        {
            AddInclude(x => x.Category);
        }
    }
}
