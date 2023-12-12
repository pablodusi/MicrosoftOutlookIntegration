using Microsoft.AspNetCore.Mvc;
using MicrosoftOutlookIntegration.Services;
using MicrosoftOutlookIntegrationApp.Entities;
using System.Diagnostics.Tracing;
using MicrosoftOutlookIntegration.DTOs;

namespace MicrosoftOutlookIntegration.Contexts
{
    public class EventController : Controller
    {
        private readonly UserDbContext _context;
        public EventController(UserDbContext context)
        {
            _context = context;
        }

        // GET /eventos

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var eventos = _context.Event.ToList();

            return Ok(eventos);
        }

        // GET /eventos/:id
        [HttpGet("eventos/id")]
        public async Task<IActionResult> Get(string id)
        {
            var evento = await _context.Event.FindAsync(id);

            if (evento == null)
            {
                return NotFound();
            }

            return Ok(evento);
        }

        [HttpPost("Eventos")]
        public async Task<CreateEventDto> CreateEvent([FromBody] Event evento)
        {
            EventService.AddEventAsync(evento);

            CreateEventCommand eventCommand = new CreateEventCommand()
            {
                EventId = evento.EventId,
                Subject = evento.Subject,
                UserId = evento.UserId,
                Body = evento.Body,
                Location = evento.Location,
                Notes = evento.Notas,
                StartTime = evento.InitialHour,
                EndTime = evento.EndHour
            };

            CreateEventCommandHandler createEventCommandHandler = new CreateEventCommandHandler(eventCommand);
            Task<CreateEventDto> t = createEventCommandHandler.HandleAsync(eventCommand);

            return t.Result;
        }

        [HttpPost("Eventos/update")]
        public async Task<UpdateEventDto> UpdateEvent([FromBody] Event evento)
        {
            EventService.UpdateEventAsync(evento);

            UpdateEventCommand eventCommand = new UpdateEventCommand()
            {
                EventId = evento.EventId,
                Subject = evento.Subject,
                UserId = evento.UserId,
                Body = evento.Body,
                Location = evento.Location,
                Notes = evento.Notas,
                StartTime = evento.InitialHour,
                EndTime = evento.EndHour
            };

            UpdateEventCommandHandler updateEventCommandHandler = new UpdateEventCommandHandler(eventCommand);
            Task<UpdateEventDto> t = updateEventCommandHandler.HandleAsync(eventCommand);

            return t.Result;
        }

        [HttpDelete("eventos/delete/id")]
        public async Task<DeleteEventDto> Delete(string id)
        {
            EventService.DeleteEventAsync(id);

            DeleteEventCommand eventCommand = new DeleteEventCommand()
            {
                EventId = id
            };

            DeleteEventCommandHandler deleteEventCommandHandler = new DeleteEventCommandHandler(eventCommand);
            Task<DeleteEventDto> t = deleteEventCommandHandler.HandleAsync(eventCommand);

            return t.Result;
        }

        [HttpPost("Eventos/list")]
        public async Task<EventDto> ListEvents([FromBody] Event evento)
        {
            ListEventQuery eventCommand = new ListEventQuery()
            {
                EventId = evento.EventId,
                Subject = evento.Subject,
                UserId = evento.UserId,
                Body = evento.Body,
                Location = evento.Location,
                Notas = evento.Notas,
                InitialHour = evento.InitialHour,
                EndHour = evento.EndHour
            };

            ListEventCommandHandler listEventCommandHandler = new ListEventCommandHandler(eventCommand);
            Task<EventDto> t = listEventCommandHandler.HandleAsync(eventCommand);

            return t.Result;
        }
    }
}

