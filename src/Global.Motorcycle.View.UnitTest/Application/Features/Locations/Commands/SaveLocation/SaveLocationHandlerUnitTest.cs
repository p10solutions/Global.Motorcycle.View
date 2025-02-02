using AutoFixture;
using AutoMapper;
using Global.Motorcycle.View.Application.Features.Locations.Commands.SaveLocation;
using Global.Motorcycle.View.Application.Features.Rentals.Commands.SaveRental;
using Global.Motorcycle.View.Domain.Contracts.Data.Repositories;
using Global.Motorcycle.View.Domain.Contracts.Notifications;
using Global.Motorcycle.View.Domain.Entities;
using Global.Motorcycle.View.Domain.Models.Notifications;
using Microsoft.Extensions.Logging;
using Moq;

namespace Global.Motorcycle.View.UnitTest.Application.Features.Locations.Commands.SaveLocation
{
    public class SaveLocationHandlerUnitTest
    {
        readonly Mock<IMotorcycleRepository> _motorcycleRepository;
        readonly Mock<ILogger<SaveRentalHandler>> _logger;
        readonly Mock<INotificationsHandler> _notificationsHandler;
        readonly Fixture _fixture;
        readonly SaveRentalHandler _handler;

        public SaveLocationHandlerUnitTest()
        {
            _motorcycleRepository = new Mock<IMotorcycleRepository>();
            _logger = new Mock<ILogger<SaveRentalHandler>>();
            _notificationsHandler = new Mock<INotificationsHandler>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new SaveRentalMapper());
            });
            IMapper mapper = mappingConfig.CreateMapper();

            _fixture = new Fixture();
            _handler = new SaveRentalHandler(_motorcycleRepository.Object, _logger.Object, mapper,
                _notificationsHandler.Object);
        }

        [Fact]
        public async Task Location_Should_Be_Created_Successfully_When_All_Information_Has_Been_Submitted()
        {
            var command = _fixture.Create<SaveRentalCommand>();

            var response = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(response);
            _motorcycleRepository.Verify(x => x.AddAsync(It.IsAny<MotorcycleEntity>()), Times.Once);
        }

        [Fact]
        public async Task Location_Should_Not_Be_Created_When_An_Exception_Was_Thrown()
        {
            var command = _fixture.Create<SaveRentalCommand>();

            _motorcycleRepository.Setup(x => x.AddAsync(It.IsAny<MotorcycleEntity>())).Throws(new Exception());
            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                    .Returns(_notificationsHandler.Object);

            var response = await _handler.Handle(command, CancellationToken.None);

            Assert.Null(response);

            _motorcycleRepository.Verify(x => x.AddAsync(It.IsAny<MotorcycleEntity>()), Times.Once);
        }
    }
}
