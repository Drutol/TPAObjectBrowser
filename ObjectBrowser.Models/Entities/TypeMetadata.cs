using System;
using System.Collections.Generic;
using System.Linq;
using ObjectBrowser.Models.Enums;

namespace ObjectBrowser.Models.Entities
{
    public class TypeMetadata
    {
        public string TypeName { get;  set; }
        public string NamespaceName { get;  set; }
        public TypeMetadata BaseType { get;  set; }
        public TypeModifiers Modifiers{ get;  set; }
        public TypeMetadata DeclaringType { get;  set; }
        public TypeKind TypeKind{ get;  set; }

        public virtual ICollection<Attribute> Attributes{ get;  set; }
        public virtual ICollection<TypeMetadata> ImplementedInterfaces{ get;  set; }
        public virtual ICollection<TypeMetadata> NestedTypes{ get;  set; }
        public virtual ICollection<PropertyMetadata> Properties{ get;  set; }
        public virtual ICollection<TypeMetadata> GenericArguments { get;  set; }
        public virtual ICollection<MethodMetadata> Methods{ get;  set; }
        public virtual ICollection<MethodMetadata> Constructors{ get;  set; }      
    }
}