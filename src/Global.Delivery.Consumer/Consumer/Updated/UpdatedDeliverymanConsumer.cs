using AutoMapper;
using Confluent.Kafka;
using Global.Delivery.View.Application.Features.Deliverys.Commands.SaveDelivery;
using Global.Motorcycle.View.Domain.Models.Events.Deliveryman;
using Global.Motorcycle.View.Infraestructure.Deserializer;
using MediatR;

namespace Global.Delivery.Consumer.Consumer.Updated
{
    public class UpdatedDeliverymanConsumer : IUpdatedDeliverymanConsumer
    {
        readonly ConsumerConfig _config;
        readonly string _topic;
        readonly IMediator _mediator;
        readonly ILogger<UpdatedDeliverymanConsumer> _logger;
        readonly IMapper _mapper;

        public UpdatedDeliverymanConsumer(IConfiguration configuration, IMediator mediator, ILogger<UpdatedDeliverymanConsumer> logger, IMapper mapper)
        {
            _config = new ConsumerConfig
            {
                BootstrapServers = configuration.GetSection("Kafka:Server").Value,
                GroupId = configuration.GetSection("Kafka:ConsumerGroupId").Value,
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };

            _topic = configuration.GetSection("Kafka:Deliveryman:UpdateTopic").Value;
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task Listen(CancellationToken cancellationToken)
        {
            using var consumer = new ConsumerBuilder<Guid, UpdatedDeliverymanEvent>(_config)
                .SetKeyDeserializer(new GuidDeserializer())
                .SetValueDeserializer(new UpdatedDeliverymanEventSerializer())
                .Build();

            consumer.Subscribe(_topic);

            _logger.LogInformation("Listening to events...");
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var updatedDeliverymanEvent = consumer.Consume(cancellationToken);

                    if (updatedDeliverymanEvent is null || updatedDeliverymanEvent.Value is null)
                        continue;

                    _logger.LogInformation("A new Deliveryman was received:");

                    _logger.LogInformation(updatedDeliverymanEvent.Value.ToString());

                    var command = _mapper.Map<SaveDeliveryCommand>(updatedDeliverymanEvent.Value);

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
