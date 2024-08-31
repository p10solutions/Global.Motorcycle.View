using Global.Motorcycle.Consumer.Consumer.Updated;

namespace Global.Motorcycle.Consumer.Worker
{
    public class WorkerUpdatedMotorcycle : BackgroundService
    {
        readonly IServiceScopeFactory _serviceScopeFactory;

        public WorkerUpdatedMotorcycle(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
            => Task.Run(async () =>
            {
                using (IServiceScope scope = _serviceScopeFactory.CreateScope())
                {
                    var consumer =
                        scope.ServiceProvider.GetRequiredService<IUpdatedMotorcycleConsumer>();

                    await consumer.Listen(stoppingToken);
                }
            }, stoppingToken);
    }
}
