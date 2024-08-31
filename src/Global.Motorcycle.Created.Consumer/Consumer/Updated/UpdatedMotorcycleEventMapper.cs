using AutoMapper;
using Global.Motorcycle.View.Application.Features.Motorcycles.Commands.SaveMotorcycle;
using Global.Motorcycle.View.Domain.Models.Events.Motorcycles;

namespace Global.Motorcycle.Consumer.Consumer.Updated
{
    public class UpdatedMotorcycleEventMapper : Profile
    {
        public UpdatedMotorcycleEventMapper()
        {
            CreateMap<UpdatedMotorcycleEvent, SaveMotorcycleCommand>();
        }
    }
}
