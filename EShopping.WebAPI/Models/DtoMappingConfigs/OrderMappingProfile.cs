using AutoMapper;
using EShopping.Core.Domain.Entities;
using EShopping.Core.Domain.ValueObjects;
using EShoppingWebAPI.Models.OrderModels;
using System.Collections.Generic;

namespace EShoppingWebAPI.Models.DtoMappingConfigs
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, OrderViewModel>();

            CreateMap<OrderSaveRequestModel, Order>()
            .ConstructUsing((src, res) =>
            {
                return new Order(src.ShippingAdress, orderItems: res.Mapper.Map<IEnumerable<OrderItem>>(src.OrderItemsDtoModel)
                );
            });


            

            CreateMap<OrderItem, OrderItemViewModel>();

            CreateMap<OrderItemSaveRequestModel, OrderItem>();

            CreateMap<PriceSaveRequestModel, Price>().ConvertUsing(x => new Price(x.Amount.Value, x.Unit.Value));
        }
    }
}
