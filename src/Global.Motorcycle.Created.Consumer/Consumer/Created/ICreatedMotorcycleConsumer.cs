namespace Global.Motorcycle.Consumer.Consumer.Created
{
    public interface ICreatedMotorcycleConsumer
    {
        Task Listen(CancellationToken cancellationToken);
    }
}