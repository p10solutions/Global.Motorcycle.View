namespace Global.Delivery.Consumer.Consumer.Updated
{
    public interface IUpdatedDeliverymanConsumer
    {
        Task Listen(CancellationToken cancellationToken);
    }
}
