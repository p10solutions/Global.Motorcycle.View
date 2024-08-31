using Global.Motorcycle.View.Domain.Entities;

namespace Global.Motorcycle.View.Domain.Contracts.Data.Repositories
{
    public interface IMotorcycleRepository
    {
        Task AddAsync(MotorcycleEntity Motorcycle);
        Task<MotorcycleEntity?> GetAsync(Guid id);
        Task<IEnumerable<MotorcycleEntity>> GetAsync(string? model, string? plate, int? year);
        Task<MotorcycleEntity> GetByDeliverymanIdAsync(Guid deliverymanId);
        Task UpdateAsync(MotorcycleEntity motorcycle);
        Task DeleteAsync(Guid id);
        Task AddDeliverymanAsync(Deliveryman deliveryman);
        Task<Deliveryman> GetDeliverymanAsync(Guid idDeliveryman);
        Task UpdateDeliverymanAsync(Deliveryman deliveryman);
    }
}
