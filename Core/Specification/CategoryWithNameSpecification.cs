using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class CategoryWithNameSpecification : BaseSpecification<Category>
    {
        public CategoryWithNameSpecification(string title): base(x => x.Title.ToLower() == title.ToLower())
        {

        }
    }
}
