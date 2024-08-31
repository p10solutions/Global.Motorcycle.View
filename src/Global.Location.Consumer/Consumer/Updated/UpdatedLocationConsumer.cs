using AutoMapper;
using Confluent.Kafka;
using Global.Motorcycle.View.Application.Features.Locations.Commands.SaveLocation;
using Global.Motorcycle.View.Domain.Models.Events.Deliveryman;
using Global.Motorcycle.View.Infraestructure.Deserializer;
using MediatR;

namespace Global.Location.Consumer.Consumer.Updated
{
    public class UpdatedLocationConsumer : IUpdatedLocationConsumer
    {
        readonly ConsumerConfig _config;
        readonly string _topic;
        readonly IMediator _mediator;
        readonly ILogger<UpdatedLocationConsumer> _logger;
        readonly IMapper _mapper;

        public UpdatedLocationConsumer(IConfiguration configuration, IMediator mediator, ILogger<UpdatedLocationConsumer> logger, IMapper mapper)
        {
            _config = new ConsumerConfig
            {
                BootstrapServers = configuration.GetSection("Kafka:Server").Value,
                GroupId = configuration.GetSection("Kafka:ConsumerGroupId").Value,
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };

            _topic = configuration.GetSection("Kafka:Location:UpdateTopic").Value;
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task Listen(CancellationToken cancellationToken)
        {
            using var consumer = new ConsumerBuilder<Guid, UpdatedLocationEvent>(_config)
                .SetKeyDeserializer(new GuidDeserializer())
                .SetValueDeserializer(new UpdatedLocationEventSerializer())
                .Build();

            consumer.Subscribe(_topic);

            _logger.LogInformation("Listening to events...");
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var updatedLocationEvent = consumer.Consume(cancellationToken);

                    if (updatedLocationEvent is null || updatedLocationEvent.Value is null)
                        continue;
                    _logger.LogInformation("A new Location was received:");

                    _logger.LogInformation(updatedLocationEvent.Value.ToString());

                    var command = _mapper.Map<SaveLocationCommand>(updatedLocationEvent.Value);

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
