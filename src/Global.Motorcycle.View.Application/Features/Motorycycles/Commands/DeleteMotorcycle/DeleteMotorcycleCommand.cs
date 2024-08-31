using Global.Motorcycle.View.Application.Features.Common;
using MediatR;

namespace Global.Motorcycle.View.Application.Features.Motorcycles.Commands.DeleteMotorcycle
{
    public class DeleteMotorcycleCommand(Guid id)
        : CommandBase<DeleteMotorcycleCommand>(new DeleteMotorcycleCommandValidator()), IRequest<DeleteMotorcycleResponse>
    {
        public Guid Id { get; set; } = id;
    }
}
