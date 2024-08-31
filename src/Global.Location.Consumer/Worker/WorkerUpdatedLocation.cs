using Global.Location.Consumer.Consumer.Updated;

namespace Global.Location.Consumer.Worker
{
    public class WorkerUpdatedLocation : BackgroundService
    {
        readonly IServiceScopeFactory _serviceScopeFactory;

        public WorkerUpdatedLocation(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
            => Task.Run(async () =>
            {
                using (IServiceScope scope = _serviceScopeFactory.CreateScope())
                {
                    var consumer =
                        scope.ServiceProvider.GetRequiredService<IUpdatedLocationConsumer>();

                    await consumer.Listen(stoppingToken);
                }
            }, stoppingToken);
    }
}
