using Global.Motorcycle.View.Api.Controllers.Base;
using Global.Motorcycle.View.Application.Features.Motorcycles.Queries.GetMotorcycle;
using Global.Motorcycle.View.Domain.Contracts.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Global.Motorcycle.View.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotorcycleController(IMediator mediator, INotificationsHandler notifications) : ApiControllerBase(mediator, notifications)
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetMotorcycleResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync([FromQuery] string? model = null, [FromQuery] string? plate = null, [FromQuery] int? year = null)
            => await SendAsync(new GetMotorcycleQuery(model, plate, year));
    }
}
