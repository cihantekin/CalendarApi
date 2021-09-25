using Calendar.Core.DataAccess.EntityFramework;
using Calendar.DataAccess.Abstract;
using Calendar.Domain;
using System;

namespace Calendar.DataAccess.Concrete
{
    public class EventDal : EntityRepositoryBase<Event, DataContext, Guid>, IEventDal
    {
    }
}
