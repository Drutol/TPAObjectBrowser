using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectBrowser.Models.Enums;

namespace ObjectBrowser.Models.Entities
{
    public class MethodModifiers
    {
        public AccessLevel AccessLevel { get; set; }
        public bool IsVirtual { get; set; }
        public bool IsAbstract { get; set; }
        public bool IsStatic { get; set; }
    }
}
