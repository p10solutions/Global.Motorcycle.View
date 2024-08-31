using AutoMapper;
using Global.Motorcycle.View.Application.Features.Common;
using Global.Motorcycle.View.Application.Features.Motorycycles.Queries.GetMotorcycle;
using Global.Motorcycle.View.Domain.Entities;

namespace Global.Motorcycle.View.Application.Features.Motorcycles.Queries.GetMotorcycle
{
    public class GetMotorcycleMapper: Profile
    {
        public GetMotorcycleMapper()
        {
            CreateMap<MotorcycleEntity, GetMotorcycleResponse>();
            CreateMap<Plan, PlanCommand>();
            CreateMap<Deliveryman, GetMotorcycleDeliverymanResponse>();
            CreateMap<Location, GetMotorcycleLocationResponse>();
        }
    }
}
