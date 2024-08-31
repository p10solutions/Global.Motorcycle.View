namespace Global.Motorcycle.View.Domain.Models.Data
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionMotorcycle { get; set; }
        public string CollectionDeliveryman { get; set; }
    }
}
