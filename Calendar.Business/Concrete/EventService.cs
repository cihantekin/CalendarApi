using Calendar.Business.Abstract;
using Calendar.Business.Dto;
using Calendar.DataAccess;
using Calendar.DataAccess.Abstract;
using Calendar.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Business.Concrete
{
    public class EventService : IEventService
    {
        private readonly IEventDal _eventDal;

        public EventService(IEventDal eventDal)
        {
            _eventDal = eventDal;
        }

        public async Task AddEvent(EventDto model)
        {
            using DataContext context = new();
            var users = await context.Users.Where(x => model.MemberNames.Select(s => s.Trim()).Contains(x.Name)).ToListAsync();
            var newUsers = model.MemberNames.Select(s => s.Trim()).Except(users.Select(s => s.Name)).ToList().Select(x => new User
            {
                Name = x
            }).ToList();

            var userList = new List<User>();
            userList.AddRange(users);
            userList.AddRange(newUsers);
            Event e = new()
            {
                Name = model.Name,
                Location = model.Location,
                Organizer = model.Organizer,
                Time = model.Time,
                Members = userList
            };

            await context.Events.AddAsync(e);
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteEvent(Guid id)
        {
            Event e = await _eventDal.GetAsync(x => x.Id == id);
            if (e == null)
            {
                return false;
            }
            await _eventDal.DeleteAsync(e);
            return true;
        }

        public async Task<EventDto> GetEvent(Guid id)
        {
            EventDto eventDto = new();
            var e = await _eventDal.GetListAsync(x => x.Id == id, i => i.Include(a => a.Members));

            if (e.Any())
            {
                eventDto = e.Select(x => new EventDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Location = x.Location,
                    Organizer = x.Organizer,
                    Time = x.Time,
                    MemberNames = x.Members?.Select(s => s.Name).ToList()
                }).FirstOrDefault();
            }

            return eventDto;
        }

        public async Task<List<EventDto>> GetEvents()
        {
            List<EventDto> eventList = new();
            IEnumerable<Event> events = await _eventDal.GetListAsync(x => !x.IsDeleted, i => i.Include(a => a.Members));
            if (events.Any())
            {
                eventList = events.Select(x => new EventDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Location = x.Location,
                    Organizer = x.Organizer,
                    Time = x.Time,
                    MemberNames = x.Members?.Select(s => s.Name)
                }).ToList();
            }

            return eventList;
        }

        public async Task<List<EventDto>> GetEventsByOrganizer(string organizer)
        {
            List<EventDto> eventList = new();
            IEnumerable<Event> events = await _eventDal.GetListAsync(x => !x.IsDeleted && x.Organizer == organizer, i => i.Include(a => a.Members));
            if (events.Any())
            {
                eventList = events.Select(x => new EventDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Location = x.Location,
                    Organizer = x.Organizer,
                    Time = x.Time,
                    MemberNames = x.Members?.Select(s => s.Name)
                }).ToList();
            }

            return eventList;
        }

        public async Task<bool> UpdateEvent(EventDto model)
        {
            using DataContext context = new();
            var e = await context.Events.Include(x => x.Members).FirstOrDefaultAsync(x => x.Id == model.Id);
            if (e == null)
            {
                return false;
            }

            var users = await context.Users.Where(x => model.MemberNames.Select(s => s.Trim()).Contains(x.Name)).ToListAsync();
            var newUsers = model.MemberNames.Select(s => s.Trim()).Except(users.Select(s => s.Name)).ToList().Select(x => new User
            {
                Name = x
            }).ToList();

            var userList = new List<User>();
            userList.AddRange(users);
            userList.AddRange(newUsers);

            e.Members.Clear();
            e.Name = model.Name;
            e.Location = model.Location;
            e.Organizer = model.Organizer;
            e.Time = model.Time;
            e.Members = userList;

            context.Update(e);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
