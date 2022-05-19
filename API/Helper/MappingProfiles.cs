using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Identity;

namespace API.Helper
{
    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Title))
                .ForMember(d => d.Image, o => o.MapFrom<ProductUrlResolver>()).ReverseMap();
            CreateMap<Core.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<CustomeBaksetDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.itemOrdered.ProductItemId))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.itemOrdered.Title))
                .ForMember(d => d.Image, o => o.MapFrom(s => s.itemOrdered.Image))
                .ForMember(d => d.Image, o => o.MapFrom<OrderItemUrlResolver>());
            CreateMap<OrderItem, OrderItemTitlesOnly>()
                .ForMember(d => d.Title, o => o.MapFrom(s => s.itemOrdered.Title));
            CreateMap<Order, OrderToReturnDtoItemTitlesOnly>()
    .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
    .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));
        }
        }
}
