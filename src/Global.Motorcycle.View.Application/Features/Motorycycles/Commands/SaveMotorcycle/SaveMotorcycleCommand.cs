using Global.Motorcycle.View.Application.Features.Common;
using Global.Motorcycle.View.Domain.Entities;
using MediatR;

namespace Global.Motorcycle.View.Application.Features.Motorcycles.Commands.SaveMotorcycle
{
    public class SaveMotorcycleCommand(Guid id, string model, string plate, int year, EMotorcycleStatus status)
        : CommandBase<SaveMotorcycleCommand>(new SaveMotorcycleCommandValidator()), IRequest<SaveMotorcycleResponse>
    {
        public Guid Id { get; set; } = id;
        public string Model { get; init; } = model;
        public string Plate { get; init; } = plate;
        public int Year { get; init; } = year;
        public EMotorcycleStatus Status { get; init; } = status;
    }
}
