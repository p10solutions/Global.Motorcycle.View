using AutoMapper;
using Global.Motorcycle.View.Application.Features.Motorcycles.Commands.SaveMotorcycle;
using Global.Motorcycle.View.Domain.Models.Events.Motorcycles;

namespace Global.Motorcycle.Consumer.Consumer.Created
{
    public class CreatedMotorcycleEventMapper : Profile
    {
        public CreatedMotorcycleEventMapper()
        {
            CreateMap<CreatedMotorcycleEvent, SaveMotorcycleCommand>();
        }
    }
}
