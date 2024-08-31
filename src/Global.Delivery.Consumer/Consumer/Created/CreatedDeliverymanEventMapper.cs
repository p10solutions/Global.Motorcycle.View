using AutoMapper;
using Global.Delivery.View.Application.Features.Deliverys.Commands.SaveDelivery;
using Global.Motorcycle.View.Domain.Models.Events.Deliveryman;

namespace Global.Deliveryman.Consumer.Consumer.Created
{
    public class CreatedDeliverymanEventMapper : Profile
    {
        public CreatedDeliverymanEventMapper()
        {
            CreateMap<CreatedDeliverymanEvent, SaveDeliveryCommand>();
        }
    }
}
