using System.Reflection;
using Autofac;
using Microsoft.Extensions.Configuration;
using Daimler.Lib.Logger;

namespace Daimler.Lib.Modules
{
    public static class ModuleHelper
    {
        public static void RegisterModules(this ContainerBuilder builder, IConfiguration configuration, Assembly assembly)
        {
            // Register your own things directly with Autofac, like:
            builder.RegisterModule(new DatabaseModule());
            builder.RegisterModule(new MediatorModule(assembly));
            builder.RegisterModule(new SerilogModule(configuration));
        }
    }
}