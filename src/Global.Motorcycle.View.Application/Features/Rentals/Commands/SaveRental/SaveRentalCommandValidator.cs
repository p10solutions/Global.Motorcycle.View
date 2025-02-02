using FluentValidation;

namespace Global.Motorcycle.View.Application.Features.Rentals.Commands.SaveRental
{
    public class SaveRentalCommandValidator : AbstractValidator<SaveRentalCommand>
    {
        public SaveRentalCommandValidator()
        {
            RuleFor(x => x.DeliverymanId).NotEmpty();
            RuleFor(x => x.MotorcycleId).NotEmpty();
            RuleFor(x => x.PlanId).NotEmpty();
        }
    }
}
