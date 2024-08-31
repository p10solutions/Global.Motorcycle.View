using Global.Motorcycle.View.Domain.Contracts.Data.Repositories;
using Global.Motorcycle.View.Domain.Contracts.Notifications;
using Global.Motorcycle.View.Domain.Models.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Global.Motorcycle.View.Application.Features.Motorcycles.Commands.DeleteMotorcycle
{
    public class DeleteMotorcycleHandler : IRequestHandler<DeleteMotorcycleCommand, DeleteMotorcycleResponse>
    {
        readonly IMotorcycleRepository _motorcycleRepository;
        readonly ILogger<DeleteMotorcycleHandler> _logger;
        readonly INotificationsHandler _notificationsHandler;

        public DeleteMotorcycleHandler(IMotorcycleRepository motorcycleRepository, ILogger<DeleteMotorcycleHandler> logger, INotificationsHandler notificationsHandler)
        {
            _motorcycleRepository = motorcycleRepository;
            _logger = logger;
            _notificationsHandler = notificationsHandler;
        }

        public async Task<DeleteMotorcycleResponse> Handle(DeleteMotorcycleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _motorcycleRepository.DeleteAsync(request.Id);

                return new DeleteMotorcycleResponse(request.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when trying to insert the Motorcycle: {Exception}", ex.Message);

                return _notificationsHandler
                     .AddNotification("An error occurred when trying to insert the Motorcycle", ENotificationType.InternalError)
                     .ReturnDefault<DeleteMotorcycleResponse>();
            }
        }
    }
}
