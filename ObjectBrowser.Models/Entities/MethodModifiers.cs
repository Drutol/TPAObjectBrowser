using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ObjectBrowser.Models.Enums;

namespace ObjectBrowser.Models.Entities
{
    [DataContract(IsReference = true)]
    public class MethodModifiers
    {
        public long Id { get; set; }
        [DataMember]
        public AccessLevel AccessLevel { get; set; }
        [DataMember]
        public bool IsVirtual { get; set; }
        [DataMember]
        public bool IsAbstract { get; set; }
        [DataMember]
        public bool IsStatic { get; set; }

        [IgnoreDataMember]
        public MethodMetadata ParentMethod { get; set; }
    }
}
