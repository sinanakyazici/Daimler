using Autofac;
using Daimler.Api.Domains.Url;
using Daimler.Api.Infrastructure.Data;
using Daimler.Lib.Database;


namespace Daimler.Api.Infrastructure
{
    public class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DaimlerContext>().As<BaseContext<Url>>().InstancePerLifetimeScope();
            builder.RegisterType<UrlRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
