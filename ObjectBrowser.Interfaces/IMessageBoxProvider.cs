using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectBrowser.Interfaces
{
    public interface IMessageBoxProvider
    {
        Task ShowMessageBoxOk(string message, string title);
    }
}
