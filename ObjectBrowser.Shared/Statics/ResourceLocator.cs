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
using ObjectBrowser.Models.Enums;
using ObjectBrowser.Shared.BL;
using ObjectBrowser.Shared.ViewModels;

namespace ObjectBrowser.Shared.Statics
{
    class Importer
    {
        private readonly CompositionContainer _compositionContainer;

        [ImportMany(typeof(IDataStorage))]
        public IEnumerable<IDataStorage> DataStorages { get; set; }

        [ImportMany(typeof(ILogger))]
        public IEnumerable<ILogger> Loggers { get; set; }

        public Importer()
        {

            //var asm = Assembly.GetAssembly(typeof(FileDataStorage));
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new ApplicationCatalog());
            _compositionContainer = new CompositionContainer(catalog);
            _compositionContainer.ComposeParts(this);
            _compositionContainer.SatisfyImportsOnce(this);
        }

    }

    public static class ResourceLocator
    {
        public delegate void AdapterRegistration(ContainerBuilder builder);

        private static ILifetimeScope _container;

        public static void RegisterDependencies(AdapterRegistration adapterDelegate)
        {
            var builder = new ContainerBuilder();


            var importer = new Importer();

            var logger = importer.Loggers.First();
            var dataStorage = importer.DataStorages.First();
            logger.Log("Starting up... Registering dependencies.", LogSeverity.Info);
            if (importer.Loggers.Count() > 1)
                logger.Log($"Detected two logging plugins, defaulting to {logger.GetType().FullName}",
                    LogSeverity.Warning);
            else
                logger.Log($"Loaded logging plugin: {logger.GetType().FullName}", LogSeverity.Info);

            if (importer.DataStorages.Count() > 1)
                logger.Log($"Detected two storage plugins, defaulting to {dataStorage.GetType().FullName}",
                    LogSeverity.Warning);
            else
                logger.Log($"Loaded storage plugin: {dataStorage.GetType().FullName}", LogSeverity.Info);

            builder.RegisterInstance(dataStorage).As<IDataStorage>().SingleInstance();
            builder.RegisterInstance(logger).As<ILogger>().SingleInstance();
            builder.RegisterType<AssemblyMetadataExtractor>().As<IAssemblyMetadataExtractor>().SingleInstance();
            builder.RegisterType<NamespaceMetadataExtractor>().As<INamespaceMetadataExtractor>().SingleInstance();
            builder.RegisterType<TypeMetadataExtractor>().As<ITypeMetadataExtractor>().SingleInstance();
            builder.RegisterType<MethodMetadataExtractor>().As<IMethodMetadataExtractor>().SingleInstance();
            builder.RegisterType<BrowserViewModel>().SingleInstance();

            adapterDelegate(builder);

            _container = builder.Build().BeginLifetimeScope();


            logger.Log("Registered all dependencies.",LogSeverity.Info);
        }

        public static ILifetimeScope ObtainScope() => _container.BeginLifetimeScope();

        public static ILogger Logger => _container.Resolve<ILogger>();

        public static IAssemblyMetadataExtractor AssemblyMetadataExtractor => _container.Resolve<IAssemblyMetadataExtractor>();
        public static INamespaceMetadataExtractor NamespaceMetadataExtractor => _container.Resolve<INamespaceMetadataExtractor>();
        public static ITypeMetadataExtractor TypeMetadataExtractor => _container.Resolve<ITypeMetadataExtractor>();
        public static IMethodMetadataExtractor MethodMetadataExtractor => _container.Resolve<IMethodMetadataExtractor>();

        public static BrowserViewModel BrowserViewModel => _container.Resolve<BrowserViewModel>();
    }
}
