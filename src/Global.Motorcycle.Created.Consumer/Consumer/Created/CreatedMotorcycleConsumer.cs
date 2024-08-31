using AutoMapper;
using Confluent.Kafka;
using Global.Motorcycle.View.Application.Features.Locations.Commands.SaveLocation;
using Global.Motorcycle.View.Application.Features.Motorcycles.Commands.SaveMotorcycle;
using Global.Motorcycle.View.Domain.Models.Events.Motorcycles;
using Global.Motorcycle.View.Infraestructure.Deserializer;
using MediatR;

namespace Global.Motorcycle.Consumer.Consumer.Created
{
    public class CreatedMotorcycleConsumer : ICreatedMotorcycleConsumer
    {
        readonly ConsumerConfig _config;
        readonly string _topic;
        readonly IMediator _mediator;
        readonly ILogger<CreatedMotorcycleConsumer> _logger;
        readonly IMapper _mapper;

        public CreatedMotorcycleConsumer(IConfiguration configuration, IMediator mediator, ILogger<CreatedMotorcycleConsumer> logger, IMapper mapper)
        {
            _config = new ConsumerConfig
            {
                BootstrapServers = configuration.GetSection("Kafka:Server").Value,
                GroupId = configuration.GetSection("Kafka:ConsumerGroupId").Value,
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };

            _topic = configuration.GetSection("Kafka:Motorcycle:CreateTopic").Value;
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task Listen(CancellationToken cancellationToken)
        {
            using var consumer = new ConsumerBuilder<Guid, CreatedMotorcycleEvent>(_config)
                .SetKeyDeserializer(new GuidDeserializer())
                .SetValueDeserializer(new CreatedMotorcycleEventSerializer())
                .Build();

            consumer.Subscribe(_topic);

            _logger.LogInformation("Listening to events...");
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var createdMotorcycleEvent = consumer.Consume(cancellationToken);

                    if (createdMotorcycleEvent is null || createdMotorcycleEvent.Value is null)
                        continue;
                    _logger.LogInformation("A new Motorcycle was received:");

                    _logger.LogInformation(createdMotorcycleEvent.Value.ToString());

                    var command = _mapper.Map<SaveMotorcycleCommand>(createdMotorcycleEvent.Value);

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
