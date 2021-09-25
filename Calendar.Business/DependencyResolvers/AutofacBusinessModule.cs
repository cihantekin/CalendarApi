using Autofac;
using Calendar.Business.Abstract;
using Calendar.Business.Concrete;
using Calendar.DataAccess.Abstract;
using Calendar.DataAccess.Concrete;

namespace Calendar.Business.DependencyResolvers
{
    public class AutofacBusinessModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EventDal>().As<IEventDal>();
            builder.RegisterType<UserDal>().As<IUserDal>();

            builder.RegisterType<EventService>().As<IEventService>();
        }
    }
}
