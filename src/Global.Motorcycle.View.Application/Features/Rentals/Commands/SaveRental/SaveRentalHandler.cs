using AutoMapper;
using Global.Motorcycle.View.Domain.Contracts.Data.Repositories;
using Global.Motorcycle.View.Domain.Contracts.Notifications;
using Global.Motorcycle.View.Domain.Entities;
using Global.Motorcycle.View.Domain.Models.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Global.Motorcycle.View.Application.Features.Rentals.Commands.SaveRental
{
    public class SaveRentalHandler : IRequestHandler<SaveRentalCommand, SaveRentalResponse>
    {
        readonly IMotorcycleRepository _motorcycleRepository;
        readonly ILogger<SaveRentalHandler> _logger;
        readonly IMapper _mapper;
        readonly INotificationsHandler _notificationsHandler;

        public SaveRentalHandler(IMotorcycleRepository motorcycleRepository, ILogger<SaveRentalHandler> logger,
            IMapper mapper, INotificationsHandler notificationsHandler)
        {
            _motorcycleRepository = motorcycleRepository;
            _logger = logger;
            _mapper = mapper;
            _notificationsHandler = notificationsHandler;
        }

        public async Task<SaveRentalResponse> Handle(SaveRentalCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var (motorcycle, IsInsert) = await GetMotorcycleAsync(request);

                await SetRentalAsync(request, motorcycle);

                if (IsInsert)
                    await _motorcycleRepository.AddAsync(motorcycle);
                else
                    await _motorcycleRepository.UpdateAsync(motorcycle);

                var response = _mapper.Map<SaveRentalResponse>(request);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when trying to save the Location: {Exception}", ex.Message);

                return _notificationsHandler
                     .AddNotification("An error occurred when trying to save the Location", ENotificationType.InternalError)
                     .ReturnDefault<SaveRentalResponse>();
            }
        }

        private async Task SetRentalAsync(SaveRentalCommand request, MotorcycleEntity motorcycle)
        {
            var rental = motorcycle.Rentals.FirstOrDefault(x => x.Id == request.Id);

            if (rental is null)
            {
                rental = _mapper.Map<Rental>(request);
                motorcycle.Rentals.Add(rental);
            }
            else
            {
                _mapper.Map(request, rental);
            }

            if (rental.Deliveryman is null)
            {
                var deliveryman = await _motorcycleRepository.GetDeliverymanAsync(request.DeliverymanId);
                rental.Deliveryman = deliveryman;
            }
        }

        private async Task<(MotorcycleEntity, bool)> GetMotorcycleAsync(SaveRentalCommand request)
        {
            var motorcycle = await _motorcycleRepository.GetAsync(request.MotorcycleId);
            var isInsert = false;
            if (motorcycle is null)
            {
                motorcycle = new MotorcycleEntity { Id = request.MotorcycleId };
                isInsert = true;
            }

            if (motorcycle.Rentals is null)
                motorcycle.Rentals = [];

            return (motorcycle, isInsert);
        }
    }
}
