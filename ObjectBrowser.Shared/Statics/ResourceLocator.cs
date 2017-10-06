using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ObjectBrowser.Models.Entities;
using ObjectBrowser.Shared.BL;
using ObjectBrowser.Shared.ViewModels;

namespace ObjectBrowser.Shared.Statics
{
    public static class ResourceLocator
    {
        private static ILifetimeScope _container;


        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();


            builder.RegisterType<AssemblyMetadataExtractor>().As<IAssemblyMetadataExtractor>().SingleInstance();
            builder.RegisterType<NamespaceMetadataExtractor>().As<INamespaceMetadataExtractor>().SingleInstance();
            builder.RegisterType<TypeMetadataExtractor>().As<ITypeMetadataExtractor>().SingleInstance();
            builder.RegisterType<MethodMetadataExtractor>().As<IMethodMetadataExtractor>().SingleInstance();

            builder.RegisterType<BrowserViewModel>().SingleInstance();

            _container = builder.Build().BeginLifetimeScope();
        }

        public static ILifetimeScope ObtainScope() => _container.BeginLifetimeScope();

        public static IAssemblyMetadataExtractor AssemblyMetadataExtractor => _container.Resolve<IAssemblyMetadataExtractor>();
        public static INamespaceMetadataExtractor NamespaceMetadataExtractor => _container.Resolve<INamespaceMetadataExtractor>();
        public static ITypeMetadataExtractor TypeMetadataExtractor => _container.Resolve<ITypeMetadataExtractor>();
        public static IMethodMetadataExtractor MethodMetadataExtractor => _container.Resolve<IMethodMetadataExtractor>();
    }
}
