namespace Global.Location.Consumer.Consumer.Created
{
    public interface ICreatedLocationConsumer
    {
        Task Listen(CancellationToken cancellationToken);
    }
}
