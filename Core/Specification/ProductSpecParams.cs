using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 50; // constant max page size 
        public int PageIndex { get; set; } = 1; //by default will always return page number 1 
        private int _pageSize = 6; // page size deafult is 6 but client can use from 1 to 50
        public int PageSize //property to enable user to deal with page size
        {
            get { return _pageSize; }
            set { _pageSize = (value > MaxPageSize) ? MaxPageSize :value ; }
        }
        public int? CategoryId { get; set; }
        public string? sort { get; set; }

        private string? _search;
        public string? Search
        {
            get { return _search; }
            set { _search = value.ToLower(); }
        }
    }
}
