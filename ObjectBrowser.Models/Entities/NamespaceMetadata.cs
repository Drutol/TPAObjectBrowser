using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace ObjectBrowser.Models.Entities
{
    [DataContract(IsReference = true)]
    public class NamespaceMetadata
    {
        public NamespaceMetadata()
        {
            
        }

        public NamespaceMetadata(string namespaceName, ICollection<TypeMetadata> types)
        {
            NamespaceName = namespaceName;
            Types = types.ToList();
        }

        [DataMember]
        public AssemblyMetadata RootAssembly { get; set; }
        public string NamespaceName { get; set; }
        [DataMember]
        public virtual List<TypeMetadata> Types { get; set; }
    }
}