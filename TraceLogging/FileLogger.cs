using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ObjectBrowser.Interfaces;
using ObjectBrowser.Models.Enums;

namespace ObjectBrowser.FileLogger
{
    [Export(typeof(ILogger))]
    public class FileLogger : ILogger
    {
        private readonly List<(string message, LogSeverity severity, DateTime time)> _trace =
            new List<(string message, LogSeverity severity, DateTime time)>();

        public void Log(string message, LogSeverity severity)
        {
            _trace.Add((message, severity, DateTime.Now));
        }

        public async Task SaveLogs(LogSeverity severity)
        {
            var entries = _trace.Where(tuple => severity.HasFlag(tuple.severity));

            using (var fs = File.CreateText(@"trace.txt"))
            {
                var str = string.Join("\n",
                    entries.Select(tuple =>
                        $"[{tuple.time}] ({tuple.severity.ToString().ToUpper()}) - {tuple.message}"));

                await fs.WriteAsync(str);
            }

            _trace.Clear();
        }
    }
}
