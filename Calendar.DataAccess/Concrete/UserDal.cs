using Calendar.Core.DataAccess.EntityFramework;
using Calendar.DataAccess.Abstract;
using Calendar.Domain;
using System;

namespace Calendar.DataAccess.Concrete
{
    public class UserDal : EntityRepositoryBase<User, DataContext, Guid>, IUserDal
    {
    }
}
