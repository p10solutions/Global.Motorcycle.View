namespace Global.Motorcycle.View.Domain.Entities
{
    public class MotorcycleEntity
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
        public int Year { get; set; }
        public EMotorcycleStatus Status { get; set; }
        public IList<Rental> Rentals { get; set; } = new List<Rental>();
    }
}
