using Autofac;
using Microsoft.AspNetCore.Http;
using Daimler.Lib.Database;

namespace Daimler.Lib.Modules
{
    public class DatabaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HttpContextAccessor>().AsImplementedInterfaces();
            builder.RegisterGeneric(typeof(BaseContext<>)).AsImplementedInterfaces().SingleInstance();
        }
    }
}