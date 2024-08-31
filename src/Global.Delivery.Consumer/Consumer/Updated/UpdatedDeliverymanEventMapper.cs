using AutoMapper;
using Global.Delivery.View.Application.Features.Deliverys.Commands.SaveDelivery;
using Global.Motorcycle.View.Domain.Models.Events.Deliveryman;

namespace Global.Delivery.Consumer.Consumer.Updated
{
    public class UpdatedDeliverymanEventMapper : Profile
    {
        public UpdatedDeliverymanEventMapper()
        {
            CreateMap<UpdatedDeliverymanEvent, SaveDeliveryCommand>();
        }
    }
}
