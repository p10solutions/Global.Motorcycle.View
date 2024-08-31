namespace Global.Motorcycle.Consumer.Consumer.Deleted
{
    public interface IDeletedMotorcycleConsumer
    {
        Task Listen(CancellationToken cancellationToken);
    }
}
