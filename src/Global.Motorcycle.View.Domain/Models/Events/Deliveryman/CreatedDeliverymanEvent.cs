using Global.Motorcycle.View.Domain.Entities;
using Global.Motorcycle.View.Domain.Models.Events.Common;

namespace Global.Motorcycle.View.Domain.Models.Events.Deliveryman
{
    public class CreatedDeliverymanEvent: Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string LicenseNumber { get; set; }
        public ELicenseType LicenseType { get; set; }
    }
}
