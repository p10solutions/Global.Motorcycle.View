using AutoMapper;
using Global.Motorcycle.View.Domain.Contracts.Data.Repositories;
using Global.Motorcycle.View.Domain.Contracts.Notifications;
using Global.Motorcycle.View.Domain.Entities;
using Global.Motorcycle.View.Domain.Models.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Global.Motorcycle.View.Application.Features.Locations.Commands.SaveLocation
{
    public class SaveLocationHandler : IRequestHandler<SaveLocationCommand, SaveLocationResponse>
    {
        readonly IMotorcycleRepository _motorcycleRepository;
        readonly ILogger<SaveLocationHandler> _logger;
        readonly IMapper _mapper;
        readonly INotificationsHandler _notificationsHandler;

        public SaveLocationHandler(IMotorcycleRepository motorcycleRepository, ILogger<SaveLocationHandler> logger,
            IMapper mapper, INotificationsHandler notificationsHandler)
        {
            _motorcycleRepository = motorcycleRepository;
            _logger = logger;
            _mapper = mapper;
            _notificationsHandler = notificationsHandler;
        }

        public async Task<SaveLocationResponse> Handle(SaveLocationCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var (motorcycle, IsInsert) = await GetMotorcycleAsync(request);

                await SetLocationAsync(request, motorcycle);

                if (IsInsert)
                    await _motorcycleRepository.AddAsync(motorcycle);
                else
                    await _motorcycleRepository.UpdateAsync(motorcycle);

                var response = _mapper.Map<SaveLocationResponse>(request);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when trying to save the Location: {Exception}", ex.Message);

                return _notificationsHandler
                     .AddNotification("An error occurred when trying to save the Location", ENotificationType.InternalError)
                     .ReturnDefault<SaveLocationResponse>();
            }
        }

        private async Task SetLocationAsync(SaveLocationCommand request, MotorcycleEntity motorcycle)
        {
            var location = motorcycle.Locations.FirstOrDefault(x => x.Id == request.Id);

            if (location is null)
            {
                location = _mapper.Map<Location>(request);
                motorcycle.Locations.Add(location);
            }
            else
            {
                _mapper.Map(request, location);
            }

            if (location.Deliveryman is null)
            {
                var deliveryman = await _motorcycleRepository.GetDeliverymanAsync(request.DeliverymanId);
                location.Deliveryman = deliveryman;
            }
        }

        private async Task<(MotorcycleEntity, bool)> GetMotorcycleAsync(SaveLocationCommand request)
        {
            var motorcycle = await _motorcycleRepository.GetAsync(request.MotorcycleId);
            var isInsert = false;
            if (motorcycle is null)
            {
                motorcycle = new MotorcycleEntity { Id = request.MotorcycleId };
                isInsert = true;
            }

            if (motorcycle.Locations is null)
                motorcycle.Locations = [];

            return (motorcycle, isInsert);
        }
    }
}
