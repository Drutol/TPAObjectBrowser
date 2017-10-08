using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ObjectBrowser.Models.Enums;
using ObjectBrowser.Shared.Statics;

namespace ObjectBrowser.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnExit(ExitEventArgs e)
        {         
            ResourceLocator.Logger.Log("Shutting down.",LogSeverity.Info);
            await ResourceLocator.Logger.SaveLogs(LogSeverity.Info|LogSeverity.Error|LogSeverity.Warning);

            base.OnExit(e);
        }
    }
}
