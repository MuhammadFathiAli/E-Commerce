using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository basketRepository;
        private readonly IMapper mapper;

        public BasketController(IBasketRepository _basketRepository, IMapper _mapper)
        {
            this.basketRepository = _basketRepository;
            mapper = _mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById (string id)
        {
            var basket = await basketRepository.GetBasketAsync (id);
            
            // if null (no basket exisit with this id, return new basket with this id)
            return Ok(basket?? new CustomerBasket(id));

        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket (CustomeBaksetDto basket)
        {
            var customerbasket = mapper.Map<CustomeBaksetDto, CustomerBasket>(basket);
            var updatedBasket = await basketRepository.UpdateBasketAsync(customerbasket);
            return Ok(updatedBasket);
        }
        [HttpDelete]
        public async Task DeleteBasket (string id)
        {
           await basketRepository.DeleteBasketAsync(id);
        }
    }
}
