using FluentValidation;

namespace Global.Motorcycle.View.Application.Features.Motorcycles.Commands.DeleteMotorcycle
{
    public class DeleteMotorcycleCommandValidator : AbstractValidator<DeleteMotorcycleCommand>
    {
        public DeleteMotorcycleCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
