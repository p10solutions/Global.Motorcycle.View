using AutoMapper;
using Confluent.Kafka;
using Global.Motorcycle.View.Application.Features.Motorcycles.Commands.DeleteMotorcycle;
using Global.Motorcycle.View.Domain.Models.Events.Motorcycles;
using Global.Motorcycle.View.Infraestructure.Deserializer;
using MediatR;


namespace Global.Motorcycle.Consumer.Consumer.Deleted
{
    public class DeletedMotorcycleConsumer : IDeletedMotorcycleConsumer
    {
        readonly ConsumerConfig _config;
        readonly string _topic;
        readonly IMediator _mediator;
        readonly ILogger<DeletedMotorcycleConsumer> _logger;
        readonly IMapper _mapper;

        public DeletedMotorcycleConsumer(IConfiguration configuration, IMediator mediator, ILogger<DeletedMotorcycleConsumer> logger, IMapper mapper)
        {
            _config = new ConsumerConfig
            {
                BootstrapServers = configuration.GetSection("Kafka:Server").Value,
                GroupId = configuration.GetSection("Kafka:ConsumerGroupId").Value,
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };

            _topic = configuration.GetSection("Kafka:Motorcycle:DeleteTopic").Value;
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task Listen(CancellationToken cancellationToken)
        {
            using var consumer = new ConsumerBuilder<Guid, DeletedMotorcycleEvent>(_config)
                .SetKeyDeserializer(new GuidDeserializer())
                .SetValueDeserializer(new DeletedMotorcycleEventSerializer())
                .Build();

            consumer.Subscribe(_topic);

            _logger.LogInformation("Listening to events...");
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var deletedMotorcycleEvent = consumer.Consume(cancellationToken);

                    if (deletedMotorcycleEvent is null || deletedMotorcycleEvent.Value is null)
                        continue;

                    _logger.LogInformation("A new Motorcycle was received:");

                    _logger.LogInformation(deletedMotorcycleEvent.Value.ToString());

                    var command = _mapper.Map<DeleteMotorcycleCommand>(deletedMotorcycleEvent.Value);

                    await _mediator.Send(command, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error ocurred when try get the event from kafka: {Message}", ex.Message);
                }
            }
        }
    }
}
