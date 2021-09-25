using Calendar.Business.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Calendar.Business.Abstract
{
    public interface IEventService
    {
        Task<List<EventDto>> GetEvents();
        Task<EventDto> GetEvent(Guid id);
        Task AddEvent(EventDto model);
        Task<bool> UpdateEvent(EventDto model);
        Task<bool> DeleteEvent(Guid id);
        Task<List<EventDto>> GetEventsByOrganizer(string organizer);
    }
}
