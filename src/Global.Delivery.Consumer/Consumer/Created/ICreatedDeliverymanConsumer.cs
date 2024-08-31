namespace Global.Deliveryman.Consumer.Consumer.Created
{
    public interface ICreatedDeliverymanConsumer
    {
        Task Listen(CancellationToken cancellationToken);
    }
}
