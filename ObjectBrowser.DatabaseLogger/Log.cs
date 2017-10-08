using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectBrowser.DatabaseLogger
{
    public class Log
    {
        public long Id { get; set; }

        public DateTime Time { get; set; }
        public string Message { get; set; }
        public string Severity { get; set; }
    }
}
