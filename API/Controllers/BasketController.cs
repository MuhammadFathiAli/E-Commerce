using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository basketRepository;

        public BasketController(IBasketRepository _basketRepository)
        {
            this.basketRepository = _basketRepository;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById (string id)
        {
            var basket = await basketRepository.GetBasketAsync (id);
            
            // if null (no basket exisit with this id, return new basket with this id)
            return Ok(basket?? new CustomerBasket(id));

        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket (CustomerBasket basket)
        {
            var updatedBasket = await basketRepository.UpdateBasketAsync(basket);
            return Ok(updatedBasket);
        }
        [HttpDelete]
        public async Task DeleteBasket (string id)
        {
           await basketRepository.DeleteBasketAsync(id);
        }
    }
}
