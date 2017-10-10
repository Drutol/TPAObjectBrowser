using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ObjectBrowser.Interfaces;

namespace ObjectBrowser.WPF.Adapters
{
    public class MessageBoxProvider : IMessageBoxProvider
    {
        public async Task ShowMessageBoxOk(string message, string title)
        {
            MessageBox.Show(message, title);
        }
    }
}
