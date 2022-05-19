using Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class OrdersWithItemsAndOrderingSpecification : BaseSpecification<Order>
    {
        public OrdersWithItemsAndOrderingSpecification(string email) : base(o => o.BuyerEmail == email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
            AddOrderByDescinding(o => o.OrderDate);

        }
        public OrdersWithItemsAndOrderingSpecification(int id, string email)
            : base(o => o.Id == id && o.BuyerEmail == email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
        }

        public OrdersWithItemsAndOrderingSpecification(int id) : base (o => o.Id == id)
        {
            AddInclude (o => o.OrderItems);
            AddInclude (o => o.DeliveryMethod);
            //    AddInclude (o => o.OrderDate);
            //    AddInclude (o => o.Status);
            //    AddInclude (o => o.BuyerEmail);
            //    AddInclude (o => o.SubTotal);
        }
        public OrdersWithItemsAndOrderingSpecification(OrderStatus status) : base(o => o.Status == status )
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
            //AddInclude(o => o.OrderDate);
            //AddInclude(o => o.Status);
            //AddInclude(o => o.BuyerEmail);
            //AddInclude(o => o.SubTotal);
        }
    }
}
