using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class ProductsWithFiltersCount  : BaseSpecification<Product>
    {
        public ProductsWithFiltersCount(ProductSpecParams productParams)
            : base (x =>
            (string.IsNullOrEmpty(productParams.Search) || x.Title.ToLower().Contains(productParams.Search)) &&
            (!productParams.CategoryId.HasValue || x.CategoryId == productParams.CategoryId)
            )
        {

        }
    }
}
