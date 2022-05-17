using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            //basket is stored as key value pair in the redis db 
            //basketId (key) : basket(value) is stored as string redis value json object
            // to retrieve data of basket (value of the key) use stringGetAsync will bk as json 
            //u need to deserialize to customerbasket 
            var data = await database.StringGetAsync(basketId);

            //if we have data we deserialize 
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
            
        }

        //updating => replace the exisitng basket with the basket coming from client 
        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var created = await database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(7));
            if (!created) return null;
            return await GetBasketAsync(basket.Id);
        }
    }
}
