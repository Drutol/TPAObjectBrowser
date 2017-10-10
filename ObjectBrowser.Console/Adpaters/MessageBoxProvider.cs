using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectBrowser.Interfaces;

namespace ObjectBrowser.Console.Adpaters
{
    public class MessageBoxProvider : IMessageBoxProvider
    {
        public async Task ShowMessageBoxOk(string message, string title)
        {
            System.Console.Write("==================");
            System.Console.WriteLine(title);
            System.Console.WriteLine(message);
            System.Console.Write("==================");
        }
    }
}
