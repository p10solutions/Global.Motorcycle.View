using Global.Motorcycle.View.Domain.Entities;

namespace Global.Delivery.View.Application.Features.Deliverys.Commands.SaveDelivery
{
    public class SaveDeliveryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string LicenseNumber { get; set; }
        public ELicenseType LicenseType { get; set; }
    }
}
