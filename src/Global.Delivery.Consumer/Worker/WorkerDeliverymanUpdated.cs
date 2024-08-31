using Global.Delivery.Consumer.Consumer.Updated;

namespace Global.Delivery.Consumer.Worker
{
    public class WorkerDeliverymanUpdated : BackgroundService
    {
        readonly IServiceScopeFactory _serviceScopeFactory;

        public WorkerDeliverymanUpdated(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
            => Task.Run(async () =>
            {
                using (IServiceScope scope = _serviceScopeFactory.CreateScope())
                {
                    var consumer =
                        scope.ServiceProvider.GetRequiredService<IUpdatedDeliverymanConsumer>();

                    await consumer.Listen(stoppingToken);
                }
            }, stoppingToken);
    }
}
