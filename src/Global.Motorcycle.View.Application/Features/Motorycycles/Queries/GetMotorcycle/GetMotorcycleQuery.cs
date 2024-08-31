using Global.Motorcycle.View.Application.Features.Common;
using MediatR;

namespace Global.Motorcycle.View.Application.Features.Motorcycles.Queries.GetMotorcycle
{
    public class GetMotorcycleQuery : CommandBase<GetMotorcycleQuery>, IRequest<IEnumerable<GetMotorcycleResponse>>
    {
        public GetMotorcycleQuery(string? model, string? plate, int? year) : base(new GetMotorcycleQueryValidator())
        {
            Model = model;
            Plate = plate;
            Year = year;
        }

        public string? Model { get; set; }
        public string? Plate { get; set; }
        public int? Year { get; set; }
    }
}
