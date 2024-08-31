using Global.Motorcycle.View.Domain.Entities;
using Global.Motorcycle.View.Domain.Models.Events.Common;

namespace Global.Motorcycle.View.Domain.Models.Events.Motorcycles
{
    public class UpdatedMotorcycleEvent: Event
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
        public int Year { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public EMotorcycleStatus Status { get; set; }
    }
}
