using Autofac;
using Autofac.Integration.WebApi;
using Backend.Domain.Modules;
using Backend.Infrastructure.Modules;
using System.Reflection;
using System.Web.Http;

namespace Backend.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Autofac setup
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule<InfrastructureModule>();
            builder.RegisterModule<DomainModule>();
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
