using System;
using System.Collections.Generic;

namespace Calendar.Domain
{
    public class User : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public IList<Event> Events{ get; set; }
    }
}
