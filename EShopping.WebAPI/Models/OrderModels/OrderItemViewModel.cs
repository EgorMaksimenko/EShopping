using EShopping.Core.Domain.ValueObjects;

namespace EShoppingWebAPI.Models.OrderModels
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public Price Price { get; set; }
    }
}
