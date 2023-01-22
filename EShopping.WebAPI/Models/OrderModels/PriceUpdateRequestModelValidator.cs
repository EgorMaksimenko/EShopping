using FluentValidation;

namespace EShoppingWebAPI.Models.OrderModels
{
    public class PriceUpdateRequestModelValidator : AbstractValidator<PriceUpdateRequestModel>
    {
        public PriceUpdateRequestModelValidator()
        {
            RuleFor(x => x.Amount)
                .NotNull();

            RuleFor(x => x.Unit)
                .NotNull()
                .IsInEnum();
        }
    }
}
