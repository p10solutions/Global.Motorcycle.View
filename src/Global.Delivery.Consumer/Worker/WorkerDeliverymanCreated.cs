using Global.Deliveryman.Consumer.Consumer.Created;

namespace Global.Delivery.Consumer.Worker
{
    public class WorkerDeliverymanCreated : BackgroundService
    {
        readonly IServiceScopeFactory _serviceScopeFactory;

        public WorkerDeliverymanCreated(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
            => Task.Run(async () =>
            {
                using (IServiceScope scope = _serviceScopeFactory.CreateScope())
                {
                    var consumer =
                        scope.ServiceProvider.GetRequiredService<ICreatedDeliverymanConsumer>();

                    await consumer.Listen(stoppingToken);
                }
            }, stoppingToken);

    }
}
