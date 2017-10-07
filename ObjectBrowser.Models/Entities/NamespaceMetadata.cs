using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjectBrowser.Models.Entities
{
    public class NamespaceMetadata
    {
        public NamespaceMetadata(string namespaceName, ICollection<TypeMetadata> types)
        {
            NamespaceName = namespaceName;
            Types = types;
        }

        public AssemblyMetadata RootAssembly { get; set; }
        public string NamespaceName { get; set; }
        public virtual ICollection<TypeMetadata> Types { get; set; }
    }
}