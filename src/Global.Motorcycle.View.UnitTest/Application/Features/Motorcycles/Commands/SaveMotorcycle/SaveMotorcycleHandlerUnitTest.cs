using AutoFixture;
using AutoMapper;
using Global.Motorcycle.View.Application.Features.Motorcycles.Commands.SaveMotorcycle;
using Global.Motorcycle.View.Domain.Contracts.Data.Repositories;
using Global.Motorcycle.View.Domain.Contracts.Notifications;
using Global.Motorcycle.View.Domain.Entities;
using Global.Motorcycle.View.Domain.Models.Notifications;
using Microsoft.Extensions.Logging;
using Moq;

namespace Global.Motorcycle.UnitTest.Application.Features.Motorcycles.Commands.SaveMotorcycle
{
    public class SaveMotorcycleHandlerUnitTest
    {
        readonly Mock<IMotorcycleRepository> _MotorcycleRepository;
        readonly Mock<ILogger<SaveMotorcycleHandler>> _logger;
        readonly Mock<INotificationsHandler> _notificationsHandler;
        readonly Fixture _fixture;
        readonly SaveMotorcycleHandler _handler;

        public SaveMotorcycleHandlerUnitTest()
        {
            _MotorcycleRepository = new Mock<IMotorcycleRepository>();
            _logger = new Mock<ILogger<SaveMotorcycleHandler>>();
            _notificationsHandler = new Mock<INotificationsHandler>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new SaveMotorcycleMapper());
            });
            IMapper mapper = mappingConfig.CreateMapper();

            _fixture = new Fixture();
            _handler = new SaveMotorcycleHandler(_MotorcycleRepository.Object, _logger.Object, mapper,
                _notificationsHandler.Object);
        }

        [Fact]
        public async Task Motorcycle_Should_Be_Created_Successfully_When_All_Information_Has_Been_Submitted()
        {
            var MotorcycleCommand = _fixture.Create<SaveMotorcycleCommand>();

            var response = await _handler.Handle(MotorcycleCommand, CancellationToken.None);

            Assert.NotNull(response);
            _MotorcycleRepository.Verify(x => x.AddAsync(It.IsAny<MotorcycleEntity>()), Times.Once);
        }

        [Fact]
        public async Task Motorcycle_Should_Not_Be_Created_When_An_Exception_Was_Thrown()
        {
            var MotorcycleCommand = _fixture.Create<SaveMotorcycleCommand>();

            _MotorcycleRepository.Setup(x => x.AddAsync(It.IsAny<MotorcycleEntity>())).Throws(new Exception());
            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                    .Returns(_notificationsHandler.Object);

            var response = await _handler.Handle(MotorcycleCommand, CancellationToken.None);

            Assert.Null(response);

            _MotorcycleRepository.Verify(x => x.AddAsync(It.IsAny<MotorcycleEntity>()), Times.Once);
        }
    }
}
