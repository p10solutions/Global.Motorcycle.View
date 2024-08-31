using AutoMapper;
using Global.Motorcycle.View.Domain.Entities;

namespace Global.Motorcycle.View.Application.Features.Motorcycles.Commands.SaveMotorcycle
{
    public class SaveMotorcycleMapper: Profile
    {
        public SaveMotorcycleMapper()
        {
            CreateMap<SaveMotorcycleCommand, MotorcycleEntity>();
            CreateMap<MotorcycleEntity, SaveMotorcycleResponse>();
        }
    }
}
