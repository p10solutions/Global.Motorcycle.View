using Global.Location.Consumer.Consumer.Created;

namespace Global.Location.Consumer.Worker
{
    public class WorkerCreatedLocation : BackgroundService
    {
        readonly IServiceScopeFactory _serviceScopeFactory;

        public WorkerCreatedLocation(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
            => Task.Run(async () =>
            {
                using (IServiceScope scope = _serviceScopeFactory.CreateScope())
                {
                    var consumer =
                        scope.ServiceProvider.GetRequiredService<ICreatedLocationConsumer>();

                    await consumer.Listen(stoppingToken);
                }
            }, stoppingToken);
    }
}
