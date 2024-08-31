namespace Global.Motorcycle.View.Application.Features.Motorcycles.Commands.DeleteMotorcycle
{
    public class DeleteMotorcycleResponse(Guid id)
    {
        
        public Guid Id { get; init; } = id;
    }
}
