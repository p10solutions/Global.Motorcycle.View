using AutoMapper;
using Global.Motorcycle.View.Application.Features.Common;
using Global.Motorcycle.View.Domain.Entities;

namespace Global.Motorcycle.View.Application.Features.Rentals.Commands.SaveRental
{
    public class SaveRentalMapper : Profile
    {
        public SaveRentalMapper()
        {
            CreateMap<SaveRentalCommand, Rental>();
            CreateMap<SaveRentalCommand, SaveRentalResponse>();
            CreateMap<PlanCommand, Plan>();
        }
    }
}
