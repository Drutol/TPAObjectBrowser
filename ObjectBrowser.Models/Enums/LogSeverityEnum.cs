using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectBrowser.Models.Enums
{
    [Flags]
    public enum LogSeverity
    {
        Info = 0x1,
        Warning = 0x2,
        Error = 0x4,
    }
}
