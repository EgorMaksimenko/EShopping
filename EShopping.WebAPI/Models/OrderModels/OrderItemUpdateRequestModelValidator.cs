using FluentValidation;

namespace EShoppingWebAPI.Models.OrderModels
{
    public class OrderItemUpdateRequestModelValidator : AbstractValidator<OrderItemUpdateRequestModel>
    {
        public OrderItemUpdateRequestModelValidator()
        {
            RuleFor(x => x.ProductId)
            .NotNull().WithMessage("Please enter a product");

            RuleFor(x => x.Price)
            .NotNull().WithMessage("Please enter price");            
        }
    }
}
