using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ObjectBrowser.Interfaces;
using ObjectBrowser.Models.Enums;

namespace ObjectBrowser.DatabaseLogger
{
    [Export(typeof(ILogger))]
    public class DatabaseLogger : ILogger
    {
        private readonly List<(string message, LogSeverity severity, DateTime time)> _trace =
            new List<(string message, LogSeverity severity, DateTime time)>();

        public DatabaseLogger()
        {
            SQLitePCL.Batteries.Init();

            using (var db = new LogDatabaseContext())
            {
                db.Database.Migrate();
            }
        }

        public void Log(string message, LogSeverity severity)
        {
            _trace.Add((message, severity, DateTime.Now));
        }

        public async Task SaveLogs(LogSeverity severity)
        {
            var entries = _trace.Where(tuple => severity.HasFlag(tuple.severity));

            using (var db = new LogDatabaseContext())
            {
                db.Logs.AddRange(entries.Select(tuple => new Log
                {
                    Message = tuple.message,
                    Severity = tuple.severity.ToString(),
                    Time = tuple.time
                }));

                await db.SaveChangesAsync();
            }
        }
    }
}
