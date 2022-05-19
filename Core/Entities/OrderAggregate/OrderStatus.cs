using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    public enum OrderStatus
    {
        [EnumMember(Value ="Pending")] //return this value instead of 0,1,2
        Pending,
        [EnumMember(Value ="Payment Recevied")]
        PaymentReceived,
        [EnumMember(Value = "Payment Failed")]
        PaymentFailed,
        [EnumMember(Value = "Order Accepted")]
        OrderAccepted,
        [EnumMember(Value = "Order Rejected")]
        OrderRejected
    }
}
