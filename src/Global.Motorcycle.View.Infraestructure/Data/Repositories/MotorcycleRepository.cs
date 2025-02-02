using Global.Motorcycle.View.Domain.Contracts.Data.Repositories;
using Global.Motorcycle.View.Domain.Entities;
using Global.Motorcycle.View.Domain.Models.Data;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Global.Motorcycle.View.Infraestructure.Data.Repositories
{
    public class MotorcycleRepository : IMotorcycleRepository
    {
        readonly IMongoCollection<MotorcycleEntity> _motoCollection;
        readonly IMongoCollection<Deliveryman> _deliverymanCollection;

        public MotorcycleRepository(IOptions<MongoDbSettings> settings)
        {
            var mongoSettings = settings.Value;
            var client = new MongoClient(mongoSettings.ConnectionString);
            var database = client.GetDatabase(mongoSettings.DatabaseName);
            _motoCollection = database.GetCollection<MotorcycleEntity>(mongoSettings.CollectionMotorcycle);
            _deliverymanCollection = database.GetCollection<Deliveryman>(mongoSettings.CollectionDeliveryman);
        }

        public async Task AddAsync(MotorcycleEntity motorcycle)
            => await _motoCollection.InsertOneAsync(motorcycle);

        public async Task<IEnumerable<MotorcycleEntity>> GetAsync(string? model, string? plate, int? year)
            => await _motoCollection
                .Find
                (
                    moto => (string.IsNullOrEmpty(model) || moto.Model == model)
                    && (string.IsNullOrEmpty(plate) || moto.Plate == plate)
                    && (!year.HasValue || moto.Year == year)
                )
                .ToListAsync();

        public async Task<MotorcycleEntity?> GetAsync(Guid id)
            => await _motoCollection.Find(moto => moto.Id == id).FirstOrDefaultAsync();

        public async Task<MotorcycleEntity> GetByDeliverymanIdAsync(Guid deliverymanId)
        {
            var filter = Builders<MotorcycleEntity>.Filter.ElemMatch(m => m.Rentals, loc => loc.DeliverymanId == deliverymanId);
            return await _motoCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(MotorcycleEntity motorcycle)
        {
            var filter = Builders<MotorcycleEntity>.Filter.Eq(m => m.Id, motorcycle.Id);
            var result = await _motoCollection.ReplaceOneAsync(filter, motorcycle);
        }

        public async Task DeleteAsync(Guid id)
        {
            var filter = Builders<MotorcycleEntity>.Filter.Eq(m => m.Id, id);
            var result = await _motoCollection.DeleteOneAsync(filter);
        }

        public async Task AddDeliverymanAsync(Deliveryman deliveryman)
            => await _deliverymanCollection.InsertOneAsync(deliveryman);

        public async Task<Deliveryman> GetDeliverymanAsync(Guid idDeliveryman)
            => await _deliverymanCollection.Find(deliveryman => deliveryman.Id == idDeliveryman).FirstOrDefaultAsync();

        public async Task UpdateDeliverymanAsync(Deliveryman deliveryman)
        {
            var filter = Builders<Deliveryman>.Filter.Eq(m => m.Id, deliveryman.Id);
            var result = await _deliverymanCollection.ReplaceOneAsync(filter, deliveryman);
        }
    }
}
