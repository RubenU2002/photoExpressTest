using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infraestructure.Models;
using WebApi.Services.Events;
using WebApi.Services.Notifications;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly IMessage _emailService;
        private readonly IEventsRepository _eventsRepository;

        public EventsController(ILogger<EventsController> logger, IEventsRepository eventsRepository, IMessage emailService)
        {
            _logger = logger;
            _eventsRepository = eventsRepository;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<Event>>>> GetAllEvents()
        {
            var response = await _eventsRepository.GetAllAsync();
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<Event>>> GetEventById(Guid id)
        {
            var response = await _eventsRepository.GetByIdAsync(id);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPost]
        public async Task<ActionResult<Response<Event>>> CreateEvent([FromBody]EventRequest eventToCreate)
        {
            
            var response = await _eventsRepository.AddAsync(eventToCreate);
            if (response is null || !response.IsSuccess || response.Data is null)
            {
                return BadRequest(response);
            }
            var eventCreatedResponse = await _eventsRepository.GetByIdAsync(response.Data.EventId);
            var eventCreated = eventCreatedResponse.Data;
            if (eventCreated is not null)
            {
                NotifyUser(eventCreated);
            }
            return CreatedAtAction(nameof(GetEventById), new { id = response?.Data?.EventId }, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response<Event>>> UpdateEvent(Guid id, EventRequest eventToUpdate)
        {
            var response = await _eventsRepository.UpdateAsync(id,eventToUpdate);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<Event>>> DeleteEvent(Guid id)
        {
            var response = await _eventsRepository.DeleteAsync(id);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return NotFound(response);
        }
        private void NotifyUser(Event eventDetails)
        {
            if(eventDetails is null || eventDetails.Institution is null)
            {
                return;
            }
            string subject="Confirmación solicitud evento";
            string body = $@"
                            <html>
                            <head>
                                <style>
                                    body {{ font-family: 'Arial', sans-serif; }}
                                    .event-details {{ border-collapse: collapse; width: 100%; }}
                                    .event-details td, .event-details th {{ border: 1px solid #ddd; padding: 8px; }}
                                    .event-details th {{ padding-top: 12px; padding-bottom: 12px; text-align: left; background-color: #4CAF50; color: white; }}
                                </style>
                            </head>
                            <body>
                                <h2>Detalles del Evento</h2>
                                <table class='event-details'>
                                    <tr><th>Numero de Estudiantes</th><td>{eventDetails.NumberOfStudents}</td></tr>
                                    <tr><th>Fecha e inicio del Evento</th><td>{eventDetails.StartTime.ToString("f")}</td></tr>
                                    <tr><th>Costo del Servicio</th><td>{eventDetails.ServiceCost:C}</td></tr>
                                    <tr><th>Toga y Birrete incluido:</th><td>{(eventDetails.CapAndGown.HasValue && eventDetails.CapAndGown.Value ? "Sí" : "No")}</td></tr>
                                    <tr><th>Nombre de la Institución</th><td>{eventDetails.Institution.InstitutionName}</td></tr>
                                </table>
                            </body>
                            </html>";
            _emailService.SendMessage(eventDetails.Institution.InstitutionAddress, subject, body);
        }
    }
}
