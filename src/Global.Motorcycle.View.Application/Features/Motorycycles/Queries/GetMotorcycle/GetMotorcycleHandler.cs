using AutoMapper;
using Global.Motorcycle.View.Domain.Contracts.Data.Repositories;
using Global.Motorcycle.View.Domain.Contracts.Notifications;
using Global.Motorcycle.View.Domain.Models.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Global.Motorcycle.View.Application.Features.Motorcycles.Queries.GetMotorcycle
{
    public class GetMotorcycleHandler : IRequestHandler<GetMotorcycleQuery, IEnumerable<GetMotorcycleResponse>>
    {
        readonly IMotorcycleRepository _motorcycleRepository;
        readonly ILogger<GetMotorcycleHandler> _logger;
        readonly INotificationsHandler _notificationsHandler;
        readonly IMapper _mapper;

        public GetMotorcycleHandler(IMotorcycleRepository MotorcycleRepository, ILogger<GetMotorcycleHandler> logger,
            INotificationsHandler notificationsHandler, IMapper mapper)
        {
            _motorcycleRepository = MotorcycleRepository;
            _logger = logger;
            _notificationsHandler = notificationsHandler;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetMotorcycleResponse>> Handle(GetMotorcycleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var motorcycles = await _motorcycleRepository.GetAsync(request.Model, request.Plate, request.Year);

                var response = _mapper.Map<IEnumerable<GetMotorcycleResponse>>(motorcycles);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when trying to get the Motorcycle: {exception}", ex.Message);

                return _notificationsHandler
                        .AddNotification("An error occurred when trying to get the Motorcycle", ENotificationType.InternalError)
                        .ReturnDefault<IEnumerable<GetMotorcycleResponse>>();
            }
        }
    }
}
