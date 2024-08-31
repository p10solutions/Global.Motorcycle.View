using AutoMapper;
using Global.Motorcycle.View.Domain.Entities;

namespace Global.Delivery.View.Application.Features.Deliverys.Commands.SaveDelivery
{
    public class SaveDeliveryMapper: Profile
    {
        public SaveDeliveryMapper()
        {
            CreateMap<SaveDeliveryCommand, Deliveryman>();
            CreateMap<Deliveryman, SaveDeliveryResponse>();
        }
    }
}
