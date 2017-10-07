using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ObjectBrowser.Models.Entities
{
    [DataContract(IsReference = true)]
    public class FieldMetadata
    {
        public long Id { get; set; }
        public FieldMetadata()
        {

        }
        public FieldMetadata(string name, TypeMetadata typeMetadata)
        {
            Name = name;
            TypeMetadata = typeMetadata;
        }

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public TypeMetadata TypeMetadata { get; set; }

        [IgnoreDataMember]
        public TypeMetadata ParentType { get; set; }
    }
}
