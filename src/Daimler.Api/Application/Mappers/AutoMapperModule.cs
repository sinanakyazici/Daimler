using Autofac;
using AutoMapper;
using AutoMapper.EquivalencyExpression;

namespace Daimler.Api.Application.Mappers
{
    public class AutoMapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new MapperConfiguration(cfg =>
            {
                cfg.AddCollectionMappers();
                cfg.AddProfile(new UrlMappers());
            }));
            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().InstancePerLifetimeScope();
        }
    }
}