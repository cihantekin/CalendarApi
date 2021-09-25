using System;
using System.Collections.Generic;

namespace Calendar.Business.Dto
{
    public class EventDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public string Location { get; set; }
        public IEnumerable<string> MemberNames { get; set; }
        public string Organizer { get; set; }
    }
}
