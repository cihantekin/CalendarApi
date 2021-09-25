using Calendar.Business.Abstract;
using Calendar.Business.Concrete;
using Calendar.DataAccess.Abstract;
using Calendar.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Calendar.Test.Business
{
    public class EventServiceTest
    {
        private readonly IEventService eventService;
        private readonly Mock<IEventDal> _mockEventDalService;

        public EventServiceTest()
        {
            _mockEventDalService = new Mock<IEventDal>();
            eventService = new EventService(_mockEventDalService.Object);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task DeleteEvent_WhenCalled_ReturnExpected(bool expected)
        {
            Event e = null;
            if (expected)
            {
                e = new();
            }
            Guid id = Guid.NewGuid();
            _mockEventDalService.Setup(x => x.GetAsync(y => y.Id == id)).ReturnsAsync(e);
            var result = await eventService.DeleteEvent(id);

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task GetEvent_WhenCalled_ReturnExpected()
        {
            List<Event> events = new()
            {
                new Event { Name = "test1" },
                new Event { Name = "test2" }
            };
            Guid id = Guid.NewGuid();
            _mockEventDalService.Setup(s => s.GetListAsync(x => x.Id == id, It.IsAny<Func<IQueryable<Event>, IIncludableQueryable<Event, object>>>())).ReturnsAsync(events);
            var result = await eventService.GetEvent(id);

            Assert.Equal("test1", result.Name);
        }

        [Fact]
        public async Task GetEventByOrganizer_WhenCalled_ReturnExpected()
        {
            List<Event> events = new()
            {
                new Event { Name = "test1", Organizer = "Cihan" },
                new Event { Name = "test2", Organizer = "Cihan" }
            };
            Guid id = Guid.NewGuid();
            _mockEventDalService.Setup(s => s.GetListAsync(It.IsAny<Expression<Func<Event, bool>>>(), It.IsAny<Func<IQueryable<Event>, IIncludableQueryable<Event, object>>>())).ReturnsAsync(events);
            var result = await eventService.GetEventsByOrganizer("Cihan");

            Assert.Equal(2, result.Count);
        }
    }
}
