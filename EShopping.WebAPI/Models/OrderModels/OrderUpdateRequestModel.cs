using System.Collections.Generic;

namespace EShoppingWebAPI.Models.OrderModels
{
    public class OrderUpdateRequestModel
    {
        /// <example>IRAN Tehran Persia</example>
        public int Id { get; set; }

        public Guid? TrackingNumber { get; set; }

        public string ShippingAdress { get; set; }

        public DateTime OrderDate { get; set; }

        public IEnumerable<OrderItemUpdateRequestModel> OrderItemsDtoModel { get; set; }
    }
}
