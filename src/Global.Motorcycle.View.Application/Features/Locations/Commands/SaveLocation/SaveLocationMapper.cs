using AutoMapper;
using Global.Motorcycle.View.Application.Features.Common;
using Global.Motorcycle.View.Domain.Entities;

namespace Global.Motorcycle.View.Application.Features.Locations.Commands.SaveLocation
{
    public class SaveLocationMapper: Profile
    {
        public SaveLocationMapper()
        {
            CreateMap<SaveLocationCommand, Location>();
            CreateMap<SaveLocationCommand, SaveLocationResponse>();
            CreateMap<PlanCommand, Plan>();
        }
    }
}
