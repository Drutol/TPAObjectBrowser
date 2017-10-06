using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectBrowser.Models.Enums;

namespace ObjectBrowser.Models.Entities
{
    public class TypeModifiers
    {
        public AccessLevel AccessLevel { get; set; }
        public bool IsSealed { get; set; }
        public bool IsAbstract { get; set; }
    }
}
