using Infraestructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Services.EventModification;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventModificationsController : ControllerBase
    {
        private readonly IEventModificationRepository _modificationRepository;

        public EventModificationsController(IEventModificationRepository modificationRepository)
        {
            _modificationRepository = modificationRepository;
        }
        [HttpGet("AllEvents/{eventId}")]
        public async Task<ActionResult<Response<List<EventModificationLog>>>> GetAllEvents(Guid eventId)
        {
            var response = await _modificationRepository.GetAllByEventIdAsync(eventId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("{modificationId}")]
        public async Task<ActionResult<Response<Event>>> GetEventById(Guid modificationId)
        {
            var response = await _modificationRepository.GetByIdAsync(modificationId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
