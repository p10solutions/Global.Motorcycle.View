using Global.Motorcycle.View.Domain.Entities;

namespace Global.Motorcycle.View.Application.Features.Motorcycles.Commands.SaveMotorcycle
{
    public class SaveMotorcycleResponse(Guid id, string model, string plate, int year, EMotorcycleStatus status)
    {
        
        public Guid Id { get; init; } = id;
        public string Model { get; init; } = model;
        public string Plate { get; init; } = plate;
        public int Year { get; init; } = year;
        public EMotorcycleStatus Status { get; init; } = status;
    }
}
