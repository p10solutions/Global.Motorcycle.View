namespace Global.Motorcycle.View.Domain.Entities
{
    public class Location
    {
        public Guid Id { get; set; }
        public Guid DeliverymanId { get; set; }
        public Guid PlanId { get; set; }
        public Guid MotorcycleId { get; set; }
        public double? Amount { get; private set; }
        public DateTime InitialDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public DateTime? ReturnDate { get; private set; }
        public bool? Paid { get; set; }
        public double? Fee { get; private set; }
        public int? DaysUse { get; private set; }
        public Plan? Plan { get; private set; }
        public Deliveryman? Deliveryman { get; set; }
        public  ELocationStatus Status { get; set; }
    }
}
