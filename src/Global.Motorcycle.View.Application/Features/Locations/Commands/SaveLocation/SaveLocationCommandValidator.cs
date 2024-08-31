using FluentValidation;

namespace Global.Motorcycle.View.Application.Features.Locations.Commands.SaveLocation
{
    public class SaveLocationCommandValidator : AbstractValidator<SaveLocationCommand>
    {
        public SaveLocationCommandValidator()
        {
            RuleFor(x => x.DeliverymanId).NotEmpty();
            RuleFor(x => x.MotorcycleId).NotEmpty();
            RuleFor(x => x.PlanId).NotEmpty();
        }
    }
}
