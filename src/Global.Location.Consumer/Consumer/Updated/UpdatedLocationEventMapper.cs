using AutoMapper;
using Global.Motorcycle.View.Application.Features.Common;
using Global.Motorcycle.View.Application.Features.Locations.Commands.SaveLocation;
using Global.Motorcycle.View.Domain.Models.Events.Common;

namespace Global.Location.Consumer.Consumer.Updated
{
    public class UpdatedLocationEventMapper : Profile
    {
        public UpdatedLocationEventMapper()
        {
            CreateMap<UpdatedLocationEvent, SaveLocationCommand>();
            CreateMap<PlanEvent, PlanCommand>();
        }
    }
}
