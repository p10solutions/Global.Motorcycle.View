using AutoMapper;
using Global.Motorcycle.View.Domain.Contracts.Data.Repositories;
using Global.Motorcycle.View.Domain.Contracts.Notifications;
using Global.Motorcycle.View.Domain.Entities;
using Global.Motorcycle.View.Domain.Models.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Global.Motorcycle.View.Application.Features.Motorcycles.Commands.SaveMotorcycle
{
    public class SaveMotorcycleHandler : IRequestHandler<SaveMotorcycleCommand, SaveMotorcycleResponse>
    {
        readonly IMotorcycleRepository _motorcycleRepository;
        readonly ILogger<SaveMotorcycleHandler> _logger;
        readonly IMapper _mapper;
        readonly INotificationsHandler _notificationsHandler;

        public SaveMotorcycleHandler(IMotorcycleRepository motorcycleRepository, ILogger<SaveMotorcycleHandler> logger,
            IMapper mapper, INotificationsHandler notificationsHandler)
        {
            _motorcycleRepository = motorcycleRepository;
            _logger = logger;
            _mapper = mapper;
            _notificationsHandler = notificationsHandler;
        }

        public async Task<SaveMotorcycleResponse> Handle(SaveMotorcycleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Year == DateTime.Now.Year)
                    _logger.LogInformation("Motorcycle of the year");

                var motorcycle = await _motorcycleRepository.GetAsync(request.Id);

                if (motorcycle is null)
                {
                    motorcycle = _mapper.Map<MotorcycleEntity>(request);
                    await _motorcycleRepository.AddAsync(motorcycle);
                }
                else
                {
                    _mapper.Map(request, motorcycle);
                    await _motorcycleRepository.UpdateAsync(motorcycle);
                }

                var response = _mapper.Map<SaveMotorcycleResponse>(motorcycle);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when trying to save the Motorcycle: {Exception}", ex.Message);

                return _notificationsHandler
                     .AddNotification("An error occurred when trying to save the Motorcycle", ENotificationType.InternalError)
                     .ReturnDefault<SaveMotorcycleResponse>();
            }
        }
    }
}
