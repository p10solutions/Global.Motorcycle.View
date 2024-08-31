using AutoFixture;
using Global.Motorcycle.View.Application.Features.Motorcycles.Commands.DeleteMotorcycle;
using Global.Motorcycle.View.Domain.Contracts.Data.Repositories;
using Global.Motorcycle.View.Domain.Contracts.Notifications;
using Global.Motorcycle.View.Domain.Models.Notifications;
using Microsoft.Extensions.Logging;
using Moq;

namespace Global.Motorcycle.View.UnitTest.Application.Features.Motorcycles.Commands.DeleteMotorcycle
{
    public class DeleteMotorcycleHandlerUnitTest
    {
        readonly Mock<IMotorcycleRepository> _motorcycleRepository;
        readonly Mock<ILogger<DeleteMotorcycleHandler>> _logger;
        readonly Mock<INotificationsHandler> _notificationsHandler;
        readonly Fixture _fixture;
        readonly DeleteMotorcycleHandler _handler;

        public DeleteMotorcycleHandlerUnitTest()
        {
            _motorcycleRepository = new Mock<IMotorcycleRepository>();
            _logger = new Mock<ILogger<DeleteMotorcycleHandler>>();
            _notificationsHandler = new Mock<INotificationsHandler>();

            _fixture = new Fixture();
            _handler = new DeleteMotorcycleHandler(_motorcycleRepository.Object, _logger.Object, _notificationsHandler.Object);
        }

        [Fact]
        public async Task Motorcycle_Should_Be_Deleted_Successfully_When_All_Information_Has_Been_Submitted()
        {
            var command = _fixture.Create<DeleteMotorcycleCommand>();

            var response = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(response);
            _motorcycleRepository.Verify(x => x.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Motorcycle_Should_Not_Be_Deleted_When_An_Exception_Was_Thrown()
        {
            var command = _fixture.Create<DeleteMotorcycleCommand>();

            _motorcycleRepository.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).Throws(new Exception());
            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                    .Returns(_notificationsHandler.Object);

            var response = await _handler.Handle(command, CancellationToken.None);

            Assert.Null(response);

            _motorcycleRepository.Verify(x => x.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}
