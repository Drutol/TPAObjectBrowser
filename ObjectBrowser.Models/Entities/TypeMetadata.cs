using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using ObjectBrowser.Models.Enums;

namespace ObjectBrowser.Models.Entities
{
    [DataContract(IsReference = true)]
    public class TypeMetadata
    {
        public TypeMetadata()
        {

        }

        public TypeMetadata(int typeHash)
        {
            TypeHash = typeHash;
        }
        [DataMember]
        public string TypeName { get;  set; }
        [DataMember]
        public string NamespaceName { get;  set; }
        [DataMember]
        public AssemblyMetadata RootAssembly { get; set; }
        [DataMember]
        public TypeMetadata BaseType { get;  set; }
        [DataMember]
        public TypeModifiers Modifiers{ get;  set; }
        [DataMember]
        public TypeMetadata DeclaringType { get;  set; }
        [DataMember]
        public TypeKind TypeKind{ get;  set; }
        [DataMember]

        public virtual List<TypeMetadata> Attributes{ get;  set; }
        [DataMember]
        public virtual List<TypeMetadata> ImplementedInterfaces{ get;  set; }
        [DataMember]
        public virtual List<TypeMetadata> NestedTypes{ get;  set; }
        [DataMember]
        public virtual List<PropertyMetadata> Properties{ get;  set; }
        [DataMember]
        public virtual List<TypeMetadata> GenericArguments { get;  set; }
        [DataMember]
        public virtual List<MethodMetadata> Methods{ get;  set; }
        [DataMember]
        public virtual List<MethodMetadata> Constructors{ get; set; }
        [DataMember]
        public virtual List<EnumFieldMetadata> EnumFields{ get; set; }
        [DataMember]
        public virtual List<FieldMetadata> Fields{ get; set; }

        [DataMember]
        public int TypeHash { get; set; }
        [DataMember]
        public bool TypeReference { get; set; }

        public override int GetHashCode()
        {
            return TypeHash;
        }
    }
}