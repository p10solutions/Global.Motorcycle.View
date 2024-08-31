namespace Global.Motorcycle.View.Domain.Entities
{
    public class MotorcycleEntity
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
        public int Year { get; set; }
        public EMotorcycleStatus Status { get; set; }
        public IList<Location> Locations { get; set; } = new List<Location>();
    }
}
