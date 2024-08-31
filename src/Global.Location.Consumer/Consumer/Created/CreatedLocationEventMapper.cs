using AutoMapper;
using Global.Motorcycle.View.Application.Features.Common;
using Global.Motorcycle.View.Application.Features.Locations.Commands.SaveLocation;
using Global.Motorcycle.View.Domain.Models.Events.Common;

namespace Global.Location.Consumer.Consumer.Created
{
    public class CreatedLocationEventMapper : Profile
    {
        public CreatedLocationEventMapper()
        {
            CreateMap<CreatedLocationEvent, SaveLocationCommand>();
            CreateMap<PlanEvent, PlanCommand>();
        }
    }
}
