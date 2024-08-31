using Global.Motorcycle.Consumer.Consumer.Deleted;

namespace Global.Motorcycle.Consumer.Worker
{
    public class WorkerDeletedMotorcycle : BackgroundService
    {
        readonly IServiceScopeFactory _serviceScopeFactory;

        public WorkerDeletedMotorcycle(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
            => Task.Run(async () =>
            {
                using (IServiceScope scope = _serviceScopeFactory.CreateScope())
                {
                    var consumer =
                        scope.ServiceProvider.GetRequiredService<IDeletedMotorcycleConsumer>();

                    await consumer.Listen(stoppingToken);
                }
            }, stoppingToken);
    }
}
