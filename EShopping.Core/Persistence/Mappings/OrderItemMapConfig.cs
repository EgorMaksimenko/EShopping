using EShopping.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Core.Persistence.Mappings
{
    public class OrderItemMapConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id).ValueGeneratedOnAdd().HasColumnName("Id");

            builder.Property(en => en.ProductId).HasColumnName("ProductId").IsRequired();

            builder.OwnsOne(en => en.Price, price =>
            {
                price.Property(x => x.Amount).HasColumnName("Amount");

                price.Property(x => x.Unit).HasColumnName("Unit");
            });
        }
    }
}
