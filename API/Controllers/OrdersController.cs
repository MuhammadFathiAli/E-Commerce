using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrdersController(IOrderService _orderService, IMapper _mapper)
        {
            orderService = _orderService;
            mapper = _mapper;
        }
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrderAsync (OrderDto orderDto)
        {
            var email = HttpContext.User.RetriveEmailFromPrincipal();
            var address = mapper.Map<AddressDto,Address>(orderDto.ShipToAddress);
            var order = await orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);
            if (order == null) return BadRequest(new ApiResponse(400, "Problem Creating Order"));
            return Ok(order); 
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
        {
            var email = HttpContext.User.RetriveEmailFromPrincipal();
            var orders = await orderService.GetOrdersForUserAsync(email);
            return Ok(mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDtoItemTitlesOnly>>(orders));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("PendingOrders")]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDtoItemTitlesOnly>>> GetAllPendingOrders()
        {
            var orders = await orderService.GetPendingOrdersAsync();
            return Ok(mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDtoItemTitlesOnly>>(orders));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdByAdmin(int id)
        {
            var order = await orderService.GetOrderByIdForAllForAsync(id);
         
            if (order == null) return BadRequest(new ApiResponse(404));
            return Ok(mapper.Map<Order, OrderToReturnDtoItemTitlesOnly>(order));
        }
        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await orderService.GetDeliveryMethodsAsync());
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPut("AcceptOrder/{id}")]
        public async Task<ActionResult<OrderToReturnDto>> AcceptOrder(int id)
        {
            var order = await orderService.GetOrderByIdForAllForAsync(id);
            if (order.Status != OrderStatus.Pending) return BadRequest(new ApiResponse(400, "Order Status is not Pending"));
            order.Status = OrderStatus.OrderAccepted;
            var updatedOrder = await orderService.UpdateOrder(order);
            if (updatedOrder == null) return BadRequest(new ApiResponse (400, "Order Couldn't be updated"));
            return Ok(mapper.Map<Order, OrderToReturnDtoItemTitlesOnly>(updatedOrder));
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("RejectOrder/{id}")]
        public async Task<ActionResult<OrderToReturnDto>> RejectOrder(int id)
        {
            var order = await orderService.GetOrderByIdForAllForAsync(id);
            if (order.Status != OrderStatus.Pending) return BadRequest(new ApiResponse(400, "Order Status is not Pending"));
            order.Status = OrderStatus.OrderRejected;
            var updatedOrder = await orderService.UpdateOrder(order);
            if (updatedOrder == null) return BadRequest(new ApiResponse(400, "Order Couldn't be updated"));
            return Ok(mapper.Map<Order, OrderToReturnDtoItemTitlesOnly>(updatedOrder));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderToReturnDtoItemTitlesOnly>> DeleteOrder(int id)
        {
            var order = await orderService.DeleteOrder(id);
            if (order == null) return BadRequest(new ApiResponse(400, "Order Couldn't be deleted"));
            return mapper.Map<Order, OrderToReturnDtoItemTitlesOnly>(order);

        }



    }
}
