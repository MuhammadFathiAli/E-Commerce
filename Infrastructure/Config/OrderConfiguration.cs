using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            //won't make validation here (required, etc..) do it in dto classes 
            //address to be owned configuration
            builder.OwnsOne(o => o.ShipToAddress, a =>
             {
                 a.WithOwner();
             });
            builder.Property(s => s.Status)
                .HasConversion(o => o.ToString(),
                o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o)
                );
            //delete orderitems when delete order 
            builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Property(p => p.SubTotal).HasColumnType("decimal");
        }
    }
}
