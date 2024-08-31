using AutoMapper;
using Confluent.Kafka;
using Global.Delivery.View.Application.Features.Deliverys.Commands.SaveDelivery;
using Global.Motorcycle.View.Domain.Models.Events.Deliveryman;
using Global.Motorcycle.View.Infraestructure.Deserializer;
using MediatR;


namespace Global.Deliveryman.Consumer.Consumer.Created
{
    public class CreatedDeliverymanConsumer : ICreatedDeliverymanConsumer
    {
        readonly ConsumerConfig _config;
        readonly string _topic;
        readonly IMediator _mediator;
        readonly ILogger<CreatedDeliverymanConsumer> _logger;
        readonly IMapper _mapper;

        public CreatedDeliverymanConsumer(IConfiguration configuration, IMediator mediator, ILogger<CreatedDeliverymanConsumer> logger, IMapper mapper)
        {
            _config = new ConsumerConfig
            {
                BootstrapServers = configuration.GetSection("Kafka:Server").Value,
                GroupId = configuration.GetSection("Kafka:ConsumerGroupId").Value,
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };

            _topic = configuration.GetSection("Kafka:Deliveryman:CreateTopic").Value;
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task Listen(CancellationToken cancellationToken)
        {
            using var consumer = new ConsumerBuilder<Guid, CreatedDeliverymanEvent>(_config)
                .SetKeyDeserializer(new GuidDeserializer())
                .SetValueDeserializer(new CreatedDeliverymanEventSerializer())
                .Build();

            consumer.Subscribe(_topic);

            _logger.LogInformation("Listening to events...");
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var createdDeliverymanEvent = consumer.Consume(cancellationToken);

                    if (createdDeliverymanEvent is null || createdDeliverymanEvent.Value is null)
                        continue;

                    _logger.LogInformation("A new Deliveryman was received:");

                    _logger.LogInformation(createdDeliverymanEvent.Value.ToString());

                    var command = _mapper.Map<SaveDeliveryCommand>(createdDeliverymanEvent.Value);

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
