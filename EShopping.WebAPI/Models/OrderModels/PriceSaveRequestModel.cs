using EShopping.Core.Domain.Enums;

namespace EShoppingWebAPI.Models.OrderModels
{
    public class PriceSaveRequestModel
    {
        /// <example>100</example>
        public int? Amount { get; set; }

        /// <example>MoneyUnit.Rial</example>
        public MoneyUnit? Unit { get; set; } = MoneyUnit.UnSpecified;
    }
}
