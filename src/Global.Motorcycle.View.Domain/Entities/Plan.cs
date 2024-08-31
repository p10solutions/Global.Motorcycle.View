namespace Global.Motorcycle.View.Domain.Entities
{
    public class Plan
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Days { get; set; }
        public double Daily { get; set; }
        public double? FeeBefore { get; set; }
        public double? FeeAfter { get; set; }
    }
}
