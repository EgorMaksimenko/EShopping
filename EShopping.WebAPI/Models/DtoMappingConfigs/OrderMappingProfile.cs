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

            CreateMap<OrderUpdateRequestModel, Order>()
                .ConstructUsing((src, res) => 
                {
                    try
                    {
                        var order = new Order(src.ShippingAdress, orderItems: res.Mapper.Map<IEnumerable<OrderItem>>(src.OrderItemsDtoModel));
                        return order;
                    }
                    catch (Exception ex)
                    { 
                    }
                    return null;
                });

            CreateMap<OrderSaveRequestModel, Order>()
            .ConstructUsing((src, res) =>
            {
                return new Order(src.ShippingAdress, orderItems: res.Mapper.Map<IEnumerable<OrderItem>>(src.OrderItemsDtoModel)
                );
            });
            
            CreateMap<OrderItem, OrderItemViewModel>();

            CreateMap<OrderItemSaveRequestModel, OrderItem>();

            CreateMap<PriceSaveRequestModel, Price>().ConvertUsing(x => new Price(x.Amount.Value, x.Unit.Value));

            CreateMap<OrderItemUpdateRequestModel, OrderItem>();

            CreateMap<PriceUpdateRequestModel, Price>().ConvertUsing(x => new Price(x.Amount.Value, x.Unit.Value));
        }
    }
}
