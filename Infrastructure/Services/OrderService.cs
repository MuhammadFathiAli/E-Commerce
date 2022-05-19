using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specification;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IBasketRepository basketRepo;

        public OrderService(IUnitOfWork _unitOfWork, IBasketRepository _basketRepo)
        {
            unitOfWork = _unitOfWork;
            basketRepo = _basketRepo;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            //get basket from basket repo
            var basket = await basketRepo.GetBasketAsync(basketId);
            
            // get items from the product repo 
            //trust only items and quantites from basket but don't trust the items price comes in the basket
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrderd = new ProductItemOrdered(productItem.Id, productItem.Title, productItem.Image);
                var orderItem = new OrderItem(itemOrderd,productItem.Price,item.Quantity);
                items.Add(orderItem);
            }
            
            //get the deliverymethod from deliverymethodrepo repo
            var dm = await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId); 
            
            //calculate subtotal based of basket items on items price from product repo
            var subtotal = items.Sum(item => item.Price * item.Quantity);
            
            //create order from order repo
            var order = new Order(items, buyerEmail, shippingAddress, dm, subtotal);
            
            unitOfWork.Repository<Order>().Add(order);
            
            //save to db 
            var results = await unitOfWork.Complete();
            
            //guarantee all changes occured on db 
            if(results <= 0 ) return null;
            
            //delete basket after order finished and saved on db
            await basketRepo.DeleteBasketAsync(basketId);
           
            //return order
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);
            return await unitOfWork.Repository<Order>().ListAsync(spec);
        }
        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(id,buyerEmail);
            return await unitOfWork.Repository<Order>().GetEntityWithSpec(spec); 
        }
        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
        }

        public async Task<Order> GetOrderByIdForAllForAsync (int id)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification (id);
            return await unitOfWork.Repository<Order>().GetEntityWithSpec (spec);
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            unitOfWork.Repository<Order>().Update(order);
            var result = await unitOfWork.Complete();

            if (result <= 0) return null;
            return order;
        }

        public async Task<Order> DeleteOrder(int id)
        {
            var order = await unitOfWork.Repository<Order>().GetByIdAsync(id);
            if (order == null) return null;
            if (order.Status != OrderStatus.Pending) return null;
            unitOfWork.Repository<Order>().Delete(order);
            var results = await unitOfWork.Complete();
            if (results <= 0) return null;
            return order;
            
        }
        public async Task<IReadOnlyList<Order>> GetPendingOrdersAsync()
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(OrderStatus.Pending);
            return await unitOfWork.Repository<Order>().ListAsync(spec);
        }
    }
}
