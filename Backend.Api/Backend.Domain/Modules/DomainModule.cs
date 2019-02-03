using Autofac;

namespace Backend.Domain.Modules
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StatsService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<CacheService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<WordsService>().AsImplementedInterfaces();
        }
    }
}
