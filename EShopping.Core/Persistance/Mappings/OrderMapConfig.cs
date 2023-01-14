using EShopping.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Core.Persistance.Mappings
{
    public class OrderMapConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id).ValueGeneratedOnAdd().HasColumnName("Id");

            builder.Property
            (en => en.TrackingNumber).HasColumnName("TrackingNumber").IsRequired(false);

            builder.HasIndex(en => en.TrackingNumber).IsUnique();

            builder.Property
            (en => en.ShippingAdress).HasColumnName
            ("ShippingAdress").HasMaxLength(100).IsUnicode().IsRequired();

            builder.Property
            (en => en.OrderDate).HasColumnName("OrderDate").HasMaxLength(10).IsRequired();
        }
    }
}
