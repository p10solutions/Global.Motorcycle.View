using Global.Motorcycle.Consumer.Consumer.Created;

namespace Global.Motorcycle.Consumer.Worker
{
    public class WorkerCreatedMotorcycle : BackgroundService
    {
        readonly IServiceScopeFactory _serviceScopeFactory;

        public WorkerCreatedMotorcycle(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
            => Task.Run(async () =>
            {
                using (IServiceScope scope = _serviceScopeFactory.CreateScope())
                {
                    var consumer =
                        scope.ServiceProvider.GetRequiredService<ICreatedMotorcycleConsumer>();

                    await consumer.Listen(stoppingToken);
                }
            }, stoppingToken);
    }
}
