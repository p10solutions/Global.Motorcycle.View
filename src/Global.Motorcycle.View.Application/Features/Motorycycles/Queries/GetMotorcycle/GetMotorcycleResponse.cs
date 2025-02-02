using Global.Motorcycle.View.Domain.Entities;

namespace Global.Motorcycle.View.Application.Features.Motorcycles.Queries.GetMotorcycle
{
    public class GetMotorcycleResponse
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
        public int Year { get; set; }
        public EMotorcycleStatus Status { get; set; }
        public IEnumerable<Rental> Locations { get; set; }
    }
}
