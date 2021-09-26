using Calendar.Business.Abstract;
using Calendar.Business.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        [Route("Calendar")]
        public async Task<IActionResult> Get()
        {
            var response = await _eventService.GetEvents();
            return Ok(response);
        }

        [HttpGet("Calendar/{id}")]
        public async Task<IActionResult> Get([Required] Guid id)
        {
            var response = await _eventService.GetEvent(id);
            return Ok(response);
        }

        [HttpGet("Calendar/Sort")]
        public async Task<IActionResult> GetOrderByDescending()
        {
            var response = await _eventService.GetEvents();
            if (!response.Any())
            {
                return NotFound();
            }
            return Ok(response.OrderByDescending(x => x.Time));
        }

        [HttpGet("{eventOrganizer}")]
        public async Task<IActionResult> GetByOrganizer(string eventOrganizer)
        {
            var response = await _eventService.GetEventsByOrganizer(eventOrganizer);
            return Ok(response);
        }

        [HttpPost]
        [Route("Calendar")]
        public async Task<IActionResult> New([Required, FromBody] EventDto e)
        {
            await _eventService.AddEvent(e);
            return Created("/api/Calendar", new { id = e.Id });
        }

        [HttpPut]
        [Route("Calendar/{id}")]
        public async Task<IActionResult> Update([Required, FromBody] EventDto e)
        {
            var isUpdated = await _eventService.UpdateEvent(e);
            if (!isUpdated)
            {
                return NotFound();
            }
            return Ok(isUpdated);
        }

        [HttpDelete]
        [Route("Calendar/{id}")]
        public async Task<IActionResult> Delete([Required] Guid id)
        {
            bool isDeleted = await _eventService.DeleteEvent(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return Ok(isDeleted);
        }
    }
}
