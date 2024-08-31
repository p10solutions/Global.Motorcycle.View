namespace Global.Motorcycle.View.Domain.Entities
{
    public class Deliveryman
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string LicenseNumber { get; set; }
        public ELicenseType LicenseType { get; set; }
        public string? LicenseImage { get; set; }
    }
}
