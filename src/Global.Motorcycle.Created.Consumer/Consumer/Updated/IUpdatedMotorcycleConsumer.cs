namespace Global.Motorcycle.Consumer.Consumer.Updated
{
    public interface IUpdatedMotorcycleConsumer
    {
        Task Listen(CancellationToken cancellationToken);
    }
}
