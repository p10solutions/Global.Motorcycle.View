using AutoMapper;
using Global.Motorcycle.View.Application.Features.Motorcycles.Commands.DeleteMotorcycle;
using Global.Motorcycle.View.Domain.Models.Events.Motorcycles;

namespace Global.Motorcycle.Consumer.Consumer.Deleted
{
    public class DeletedMotorcycleEventMapper : Profile
    {
        public DeletedMotorcycleEventMapper()
        {
            CreateMap<DeletedMotorcycleEvent, DeleteMotorcycleCommand>();
        }
    }
}
