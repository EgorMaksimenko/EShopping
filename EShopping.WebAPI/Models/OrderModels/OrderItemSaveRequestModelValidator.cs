using FluentValidation;

namespace EShoppingWebAPI.Models.OrderModels
{
    public class OrderItemSaveRequestModelValidator : AbstractValidator<OrderItemSaveRequestModel>
    {
        public OrderItemSaveRequestModelValidator()
        {
            RuleFor(x => x.ProductId)
            .NotNull().WithMessage("Please enter a product");

            RuleFor(x => x.Price)
            .NotNull().WithMessage("Please enter price");            
        }
    }
}
