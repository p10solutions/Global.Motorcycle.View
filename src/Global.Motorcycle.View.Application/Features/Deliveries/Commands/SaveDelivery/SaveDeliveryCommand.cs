using Global.Motorcycle.View.Application.Features.Common;
using Global.Motorcycle.View.Domain.Entities;
using MediatR;

namespace Global.Delivery.View.Application.Features.Deliverys.Commands.SaveDelivery
{
    public class SaveDeliveryCommand : CommandBase<SaveDeliveryCommand>, IRequest<SaveDeliveryResponse>
    {
        public SaveDeliveryCommand() : base(new SaveDeliveryCommandValidator())
        {

        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string LicenseNumber { get; set; }
        public ELicenseType LicenseType { get; set; }
        public string? LicenseImage { get; set; }
    }
}
