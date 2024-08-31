namespace Global.Location.Consumer.Consumer.Updated
{
    public interface IUpdatedLocationConsumer
    {
        Task Listen(CancellationToken cancellationToken);
    }
}
