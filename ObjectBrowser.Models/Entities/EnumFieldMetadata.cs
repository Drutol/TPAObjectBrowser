using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ObjectBrowser.Models.Entities
{
    [DataContract(IsReference = true)]
    public class EnumFieldMetadata
    {
        public EnumFieldMetadata()
        {
            
        }

        public EnumFieldMetadata(string name, int value)
        {
            Name = name;
            Value = value;
        }

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Value { get; set; }
    }
}
