using FluentValidation;

namespace EShoppingWebAPI.Models.OrderModels
{
    public class OrderUpdateRequestModelValidator : AbstractValidator<OrderUpdateRequestModel>
    {
        public OrderUpdateRequestModelValidator()
        {
            RuleFor(x => x.ShippingAdress)
           .NotNull()
           .NotEmpty()
           .Length(2, 100);

            RuleFor(x => x.OrderItemsDtoModel)
           .NotNull().WithMessage("Please enter order items!");
        }
    }
}
