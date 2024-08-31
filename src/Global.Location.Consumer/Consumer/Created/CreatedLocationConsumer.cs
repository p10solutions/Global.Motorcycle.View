using AutoMapper;
using Confluent.Kafka;
using Global.Motorcycle.View.Application.Features.Locations.Commands.SaveLocation;
using Global.Motorcycle.View.Infraestructure.Deserializer;
using MediatR;

namespace Global.Location.Consumer.Consumer.Created
{
    public class CreatedLocationConsumer : ICreatedLocationConsumer
    {
        readonly ConsumerConfig _config;
        readonly string _topic;
        readonly IMediator _mediator;
        readonly ILogger<CreatedLocationConsumer> _logger;
        readonly IMapper _mapper;

        public CreatedLocationConsumer(IConfiguration configuration, IMediator mediator, ILogger<CreatedLocationConsumer> logger, IMapper mapper)
        {
            _config = new ConsumerConfig
            {
                BootstrapServers = configuration.GetSection("Kafka:Server").Value,
                GroupId = configuration.GetSection("Kafka:ConsumerGroupId").Value,
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };

            _topic = configuration.GetSection("Kafka:Location:CreateTopic").Value;
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task Listen(CancellationToken cancellationToken)
        {
            using var consumer = new ConsumerBuilder<Guid, CreatedLocationEvent>(_config)
                .SetKeyDeserializer(new GuidDeserializer())
                .SetValueDeserializer(new CreatedLocationEventSerializer())
                .Build();

            consumer.Subscribe(_topic);

            _logger.LogInformation("Listening to events...");
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var createdLocationEvent = consumer.Consume(cancellationToken);

                    if (createdLocationEvent is null || createdLocationEvent.Value is null)
                        continue;

                    _logger.LogInformation("A new Location was received:");

                    _logger.LogInformation(createdLocationEvent.Value.ToString());

                    var command = _mapper.Map<SaveLocationCommand>(createdLocationEvent.Value);

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
