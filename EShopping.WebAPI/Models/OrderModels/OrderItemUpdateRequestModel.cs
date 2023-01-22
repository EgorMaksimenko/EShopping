using EShopping.Core.Domain.ValueObjects;

namespace EShoppingWebAPI.Models.OrderModels
{
    public class OrderItemUpdateRequestModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public PriceUpdateRequestModel Price { get; set; }
    }
}
