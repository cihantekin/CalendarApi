using System;
using System.Collections.Generic;

namespace Calendar.Domain
{
    public class Event:BaseEntity<Guid>
    {
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public string Location { get; set; }
        public IList<User> Members { get; set; }
        public string Organizer { get; set; }
    }
}
