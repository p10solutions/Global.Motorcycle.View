using AutoFixture;
using AutoMapper;
using Global.Delivery.View.Application.Features.Deliverys.Commands.SaveDelivery;
using Global.Motorcycle.View.Domain.Contracts.Data.Repositories;
using Global.Motorcycle.View.Domain.Contracts.Notifications;
using Global.Motorcycle.View.Domain.Entities;
using Global.Motorcycle.View.Domain.Models.Notifications;
using Microsoft.Extensions.Logging;
using Moq;

namespace Global.Motorcycle.View.UnitTest.Application.Features.Deliveries.Commands.SaveDelivery
{
    public class SaveDeliveryHandlerUnitTest
    {
        readonly Mock<IMotorcycleRepository> _motorcycleRepository;
        readonly Mock<ILogger<SaveDeliveryHandler>> _logger;
        readonly Mock<INotificationsHandler> _notificationsHandler;
        readonly Fixture _fixture;
        readonly SaveDeliveryHandler _handler;

        public SaveDeliveryHandlerUnitTest()
        {
            _motorcycleRepository = new Mock<IMotorcycleRepository>();
            _logger = new Mock<ILogger<SaveDeliveryHandler>>();
            _notificationsHandler = new Mock<INotificationsHandler>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new SaveDeliveryMapper());
            });
            IMapper mapper = mappingConfig.CreateMapper();

            _fixture = new Fixture();
            _handler = new SaveDeliveryHandler(_motorcycleRepository.Object, _logger.Object, mapper,
                _notificationsHandler.Object);
        }

        [Fact]
        public async Task Deliveryman_Should_Be_Created_Successfully_When_All_Information_Has_Been_Submitted()
        {
            var command = _fixture.Create<SaveDeliveryCommand>();

            var response = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(response);
            _motorcycleRepository.Verify(x => x.AddDeliverymanAsync(It.IsAny<Deliveryman>()), Times.Once);
        }

        [Fact]
        public async Task Deliveryman_Should_Not_Be_Created_When_An_Exception_Was_Thrown()
        {
            var command = _fixture.Create<SaveDeliveryCommand>();

            _motorcycleRepository.Setup(x => x.AddDeliverymanAsync(It.IsAny<Deliveryman>())).Throws(new Exception());
            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                    .Returns(_notificationsHandler.Object);

            var response = await _handler.Handle(command, CancellationToken.None);

            Assert.Null(response);

            _motorcycleRepository.Verify(x => x.AddDeliverymanAsync(It.IsAny<Deliveryman>()), Times.Once);
        }
    }
}
