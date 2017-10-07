using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ObjectBrowser.Models.Entities
{
    public class AssemblyMetadata
    {
        public string Name { get; set; }
        public virtual ICollection<NamespaceMetadata> Namespaces { get; set; }

        public virtual ICollection<TypeMetadata> RegisteredTypes { get; set; } = new List<TypeMetadata>();
    }
}