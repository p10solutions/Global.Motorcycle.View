using AutoMapper;
using Global.Motorcycle.View.Domain.Contracts.Data.Repositories;
using Global.Motorcycle.View.Domain.Contracts.Notifications;
using Global.Motorcycle.View.Domain.Entities;
using Global.Motorcycle.View.Domain.Models.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Global.Delivery.View.Application.Features.Deliverys.Commands.SaveDelivery
{
    public class SaveDeliveryHandler : IRequestHandler<SaveDeliveryCommand, SaveDeliveryResponse>
    {
        readonly IMotorcycleRepository _motorcycleRepository;
        readonly ILogger<SaveDeliveryHandler> _logger;
        readonly IMapper _mapper;
        readonly INotificationsHandler _notificationsHandler;

        public SaveDeliveryHandler(IMotorcycleRepository motorcycleRepository, ILogger<SaveDeliveryHandler> logger,
            IMapper mapper, INotificationsHandler notificationsHandler)
        {
            _motorcycleRepository = motorcycleRepository;
            _logger = logger;
            _mapper = mapper;
            _notificationsHandler = notificationsHandler;
        }

        public async Task<SaveDeliveryResponse> Handle(SaveDeliveryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deliveryman = await SaveDeliverymanAsync(request);

                await SaveMotorcycleAsync(deliveryman);

                var response = _mapper.Map<SaveDeliveryResponse>(deliveryman);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when trying to save the Deliveryman: {Exception}", ex.Message);

                return _notificationsHandler
                     .AddNotification("An error occurred when trying to save the Deliveryman", ENotificationType.InternalError)
                     .ReturnDefault<SaveDeliveryResponse>();
            }
        }

        private async Task SaveMotorcycleAsync(Deliveryman deliveryman)
        {
            var motorcycle = await _motorcycleRepository
                .GetByDeliverymanIdAsync(deliveryman.Id);

            if (motorcycle is null)
                return;

            var location = motorcycle
                .Rentals
                .FirstOrDefault(x => x.DeliverymanId == deliveryman.Id);

            location.Deliveryman = deliveryman;

            await _motorcycleRepository
                .UpdateAsync(motorcycle);
        }

        private async Task<Deliveryman> SaveDeliverymanAsync(SaveDeliveryCommand request)
        {
            var deliveryman = await _motorcycleRepository
                .GetDeliverymanAsync(request.Id);

            if (deliveryman is null)
            {
                deliveryman = _mapper.Map<Deliveryman>(request);
                await _motorcycleRepository
                    .AddDeliverymanAsync(deliveryman);
            }
            else
            {
                _mapper.Map(request, deliveryman);
                await _motorcycleRepository
                    .UpdateDeliverymanAsync(deliveryman);
            }

            return deliveryman;
        }
    }
}
