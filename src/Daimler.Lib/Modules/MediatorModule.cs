using System.Reflection;
using Autofac;
using MediatR;

namespace Daimler.Lib.Modules
{
    public class MediatorModule : Autofac.Module
    {
        private readonly Assembly _assembly;

        public MediatorModule(Assembly assembly)
        {
            _assembly = assembly;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(_assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
        }
    }
}