using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using Newtonsoft.Json;
using ObjectBrowser.Interfaces;
using ObjectBrowser.Models;
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
        private AppConfiguration _configuration;

        protected override async void OnStartup(StartupEventArgs e)
        {
            try
            {
                ResourceLocator.RegisterDependencies(AdapterDelegate);
            }
            catch (Exception)
            {
                await new MessageBoxProvider().ShowMessageBoxOk("Unable to find one or more plugins, terminating.",
                    "Error");
                Current.Shutdown();
            }

            try
            {
                _configuration =
                    JsonConvert.DeserializeObject<AppConfiguration>(File.ReadAllText(@"configuration.json"));
            }
            catch (Exception ex)
            {
                _configuration = new AppConfiguration
                {
                    LogErrors = true,
                    LogWarning = true,
                    LogInfo = false,
                };
                ResourceLocator.Logger.Log($"Unable to find configuration file, using default one.\n{ex}",LogSeverity.Warning);
            }


            base.OnStartup(e);
        }

        private void AdapterDelegate(ContainerBuilder builder)
        {
            builder.RegisterType<MessageBoxProvider>().As<IMessageBoxProvider>();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            LogSeverity severities = LogSeverity.None;
            if (_configuration.LogInfo)
                severities |= LogSeverity.Info;
            if (_configuration.LogWarning)
                severities |= LogSeverity.Warning;
            if (_configuration.LogErrors)
                severities |= LogSeverity.Error;
            ResourceLocator.Logger.Log("Shutting down.",LogSeverity.Info);
            await ResourceLocator.Logger.SaveLogs(severities);

            base.OnExit(e);
        }
    }
}
