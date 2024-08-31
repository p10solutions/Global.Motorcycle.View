using Global.Motorcycle.View.Domain.Models.Events.Common;

namespace Global.Motorcycle.View.Domain.Models.Events.Motorcycles
{
    public class DeletedMotorcycleEvent(Guid id): Event
    {        
        public Guid Id { get; init; } = id;
    }
}
