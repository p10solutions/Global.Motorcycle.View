using AutoFixture;
using AutoMapper;
using Global.Motorcycle.View.Application.Features.Motorcycles.Queries.GetMotorcycle;
using Global.Motorcycle.View.Domain.Contracts.Data.Repositories;
using Global.Motorcycle.View.Domain.Contracts.Notifications;
using Global.Motorcycle.View.Domain.Entities;
using Global.Motorcycle.View.Domain.Models.Notifications;
using Microsoft.Extensions.Logging;
using Moq;

namespace Global.Motorcycle.UnitTest.Application.Features.Motorcycles.Queries.GetById
{
    public class GetMotorcycleUnitTest
    {
        readonly Mock<IMotorcycleRepository> _motorcycleRepository;
        readonly Mock<ILogger<GetMotorcycleHandler>> _logger;
        readonly Mock<INotificationsHandler> _notificationsHandler;
        readonly Fixture _fixture;
        readonly GetMotorcycleHandler _handler;
        public GetMotorcycleUnitTest()
        {
            _motorcycleRepository = new Mock<IMotorcycleRepository>();
            _logger = new Mock<ILogger<GetMotorcycleHandler>>();
            _notificationsHandler = new Mock<INotificationsHandler>();
            _fixture = new Fixture();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new GetMotorcycleMapper());
            });
            IMapper mapper = mappingConfig.CreateMapper();

            _handler = new GetMotorcycleHandler(_motorcycleRepository.Object, _logger.Object, 
                _notificationsHandler.Object, mapper);
        }

        [Fact]
        public async Task Motorcycle_Should_Be_Geted_Successfully_When_All_Information_Has_Been_Submitted()
        {
            var motorcycleQuery = _fixture.Create<GetMotorcycleQuery>();
            var motorcycles = _fixture.CreateMany<MotorcycleEntity>();

            _motorcycleRepository.Setup(x => x.GetAsync(It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<int?>())).ReturnsAsync(motorcycles);

            var response = await _handler.Handle(motorcycleQuery, CancellationToken.None);

            Assert.NotNull(response);
            _motorcycleRepository.Verify(x => x.GetAsync(It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<int?>()), Times.Once);
        }


        [Fact]
        public async Task Motorcycle_Should_Not_Be_Geted_When_Motorcycle_Not_Found()
        {
            var motorcycleQuery = _fixture.Create<GetMotorcycleQuery>();
            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                .Returns(_notificationsHandler.Object);

            var response = await _handler.Handle(motorcycleQuery, CancellationToken.None);

            Assert.Empty(response);
            _motorcycleRepository.Verify(x => x.GetAsync(It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<int?>()), Times.Once);
        }

        [Fact]
        public async Task Motorcycle_Should_Not_Be_Geted_When_An_Exception_Was_Thrown()
        {
            var motorcycleQuery = _fixture.Create<GetMotorcycleQuery>();
            _motorcycleRepository.Setup(x => x.GetAsync(It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<int?>())).Throws(new Exception());
            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                    .Returns(_notificationsHandler.Object);

            var response = await _handler.Handle(motorcycleQuery, CancellationToken.None);

            Assert.Empty(response);
            _motorcycleRepository.Verify(x => x.GetAsync(It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<int?>()), Times.Once);
        }
    }
}
