using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace ObjectBrowser.Models.Entities
{
    [DataContract(IsReference = true)]
    public class AssemblyMetadata
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public virtual List<NamespaceMetadata> Namespaces { get; set; }
        [DataMember]
        public virtual List<TypeMetadata> RegisteredTypes { get; set; } = new List<TypeMetadata>();
    }
}