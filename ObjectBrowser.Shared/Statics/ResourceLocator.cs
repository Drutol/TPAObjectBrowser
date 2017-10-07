using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ObjectBrowser.DataAccess;
using ObjectBrowser.FileStorage;
using ObjectBrowser.Interfaces;
using ObjectBrowser.Models.Entities;
using ObjectBrowser.Shared.BL;
using ObjectBrowser.Shared.ViewModels;

namespace ObjectBrowser.Shared.Statics
{
    class Importer
    {
        private CompositionContainer _compositionContainer;
        //private CompositionHost _compositionContainer;



        [Import(typeof(IDataStorage))]
        public IDataStorage DataStorage { get; set; }

        public Importer()
        {

            //var asm = Assembly.GetAssembly(typeof(FileDataStorage));
            var asm = Assembly.GetAssembly(typeof(DatabaseDataStorage));
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(asm));
            _compositionContainer = new CompositionContainer(catalog);


            _compositionContainer.ComposeParts(this);

            _compositionContainer.SatisfyImportsOnce(this);
        }

    }

    public static class ResourceLocator
    {
        private static ILifetimeScope _container;



        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

 
            var importer = new Importer();

            builder.RegisterInstance(importer.DataStorage).As<IDataStorage>().SingleInstance();
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
