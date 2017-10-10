using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using ObjectBrowser.Interfaces;
using ObjectBrowser.Models.Enums;
using ObjectBrowser.Shared.Statics;
using ObjectBrowser.WPF.Adapters;

namespace ObjectBrowser.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ResourceLocator.RegisterDependencies(AdapterDelegate);

            base.OnStartup(e);
        }

        private void AdapterDelegate(ContainerBuilder builder)
        {
            builder.RegisterType<MessageBoxProvider>().As<IMessageBoxProvider>();
        }

        protected override async void OnExit(ExitEventArgs e)
        {         
            ResourceLocator.Logger.Log("Shutting down.",LogSeverity.Info);
            await ResourceLocator.Logger.SaveLogs(LogSeverity.Info|LogSeverity.Error|LogSeverity.Warning);

            base.OnExit(e);
        }
    }
}
