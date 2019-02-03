using Autofac;
using Backend.Infrastructure.Model;

namespace Backend.Infrastructure.Modules
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WordsDataContextFactory>().AsImplementedInterfaces();
            builder.RegisterType<SessionRepository>().AsImplementedInterfaces();
            builder.RegisterType<WordsRepository>().AsImplementedInterfaces();
        }
    }
}
