using Calendar.Core.DataAccess;
using Calendar.Domain;
using System;

namespace Calendar.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User, Guid>
    {
    }
}
