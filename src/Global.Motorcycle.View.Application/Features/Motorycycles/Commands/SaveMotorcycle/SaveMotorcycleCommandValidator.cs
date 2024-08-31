using FluentValidation;

namespace Global.Motorcycle.View.Application.Features.Motorcycles.Commands.SaveMotorcycle
{
    public class SaveMotorcycleCommandValidator : AbstractValidator<SaveMotorcycleCommand>
    {
        public SaveMotorcycleCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Model).NotEmpty();
            RuleFor(x => x.Model).Length(2, 200);
            RuleFor(x => x.Plate).NotEmpty();
            RuleFor(x => x.Plate).Length(2, 10);
            RuleFor(x => x.Year).NotEmpty();
            RuleFor(x => x.Status).NotEmpty();
        }
    }
}
