using AutoMapper;
using Confluent.Kafka;
using Global.Motorcycle.View.Application.Features.Motorcycles.Commands.SaveMotorcycle;
using Global.Motorcycle.View.Domain.Models.Events.Motorcycles;
using Global.Motorcycle.View.Infraestructure.Deserializer;
using MediatR;


namespace Global.Motorcycle.Consumer.Consumer.Updated
{
    public class UpdatedMotorcycleConsumer : IUpdatedMotorcycleConsumer
    {
        readonly ConsumerConfig _config;
        readonly string _topic;
        readonly IMediator _mediator;
        readonly ILogger<UpdatedMotorcycleConsumer> _logger;
        readonly IMapper _mapper;

        public UpdatedMotorcycleConsumer(IConfiguration configuration, IMediator mediator, ILogger<UpdatedMotorcycleConsumer> logger, IMapper mapper)
        {
            _config = new ConsumerConfig
            {
                BootstrapServers = configuration.GetSection("Kafka:Server").Value,
                GroupId = configuration.GetSection("Kafka:ConsumerGroupId").Value,
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };

            _topic = configuration.GetSection("Kafka:Motorcycle:UpdateTopic").Value;
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task Listen(CancellationToken cancellationToken)
        {
            using var consumer = new ConsumerBuilder<Guid, UpdatedMotorcycleEvent>(_config)
                .SetKeyDeserializer(new GuidDeserializer())
                .SetValueDeserializer(new UpdatedMotorcycleEventSerializer())
                .Build();

            consumer.Subscribe(_topic);

            _logger.LogInformation("Listening to events...");
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var updatedMotorcycleEvent = consumer.Consume(cancellationToken);

                    if (updatedMotorcycleEvent is null || updatedMotorcycleEvent.Value is null)
                        continue;

                    _logger.LogInformation("A new Motorcycle was received");

                    _logger.LogInformation(updatedMotorcycleEvent.Value.ToString());

                    var command = _mapper.Map<SaveMotorcycleCommand>(updatedMotorcycleEvent.Value);

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
