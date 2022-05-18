using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helper
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration config;

        public ProductUrlResolver(IConfiguration _config)
        {
            this.config = _config;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Image))
            {
                return config["ApiUrl"] + source.Image;
            }
            return null;
        }
    }
}
