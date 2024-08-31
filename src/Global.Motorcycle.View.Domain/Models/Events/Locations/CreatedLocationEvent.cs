using Global.Motorcycle.View.Domain.Entities;
using Global.Motorcycle.View.Domain.Models.Events.Common;

namespace Global.Motorcycle.View.Application.Features.Locations.Commands.SaveLocation
{
    public class CreatedLocationEvent: Event
    {
        public Guid Id { get; set; }
        public Guid DeliverymanId { get; set; }
        public Guid PlanId { get; set; }
        public Guid MotorcycleId { get; set; }
        public double? Amount { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool? Paid { get; set; }
        public double? Fee { get; set; }
        public int? DaysUse { get; set; }
        public PlanEvent? Plan { get; set; }
        public ELocationStatus Status { get; set; }
    }
}
