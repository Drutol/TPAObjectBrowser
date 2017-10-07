using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace ObjectBrowser.Models.Entities
{
    [DataContract(IsReference = true)]
    public class PropertyMetadata
    {
        public PropertyMetadata()
        {

        }

        public PropertyMetadata(string name, TypeMetadata typeMetadata)
        {
            Name = name;
            TypeMetadata = typeMetadata;
        }
        [DataMember]
        public AssemblyMetadata RootAssembly { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public TypeMetadata TypeMetadata { get; set; }
    }
}