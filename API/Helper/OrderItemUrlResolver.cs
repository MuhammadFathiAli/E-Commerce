using API.Dtos;
using AutoMapper;
using Core.Entities.OrderAggregate;

namespace API.Helper
{
    public class OrderItemUrlResolver : IValueResolver<OrderItem,OrderItemDto, string>
    {
        private readonly IConfiguration config;

        public OrderItemUrlResolver(IConfiguration _config)
        {
            config = _config;
        }

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.itemOrdered.Image))
            {
                return config["ApiUrl"] + source.itemOrdered.Image;
            }
            return null;
        }
    }
}
