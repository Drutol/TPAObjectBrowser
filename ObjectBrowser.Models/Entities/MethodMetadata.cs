using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using ObjectBrowser.Models.Enums;

namespace ObjectBrowser.Models.Entities
{
    [DataContract(IsReference = true)]
    public class MethodMetadata
    {
        //vars
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public MethodModifiers Modifiers { get; set; }
        [DataMember]
        public AssemblyMetadata RootAssembly { get; set; }
        [DataMember]
        public TypeMetadata ReturnType { get; set; }
        [DataMember]
        public bool Extension { get; set; }
        [DataMember]
        public virtual List<TypeMetadata> GenericArguments { get; set; }
        [DataMember]
        public virtual List<ParameterMetadata> Parameters { get; set; }
    }
}