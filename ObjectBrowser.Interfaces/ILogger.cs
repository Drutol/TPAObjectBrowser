using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectBrowser.Models.Enums;

namespace ObjectBrowser.Interfaces
{
    public interface ILogger
    {
        void Log(string message, LogSeverity severity);
        Task SaveLogs (LogSeverity severity);
    }
}
