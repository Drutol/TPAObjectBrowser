using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using ObjectBrowser.Models.Enums;

namespace ObjectBrowser.Models.Entities
{
    public class MethodMetadata
    {
        //vars
        public string Name { get; set; }
        public MethodModifiers Modifiers { get; set; }
        public TypeMetadata ReturnType { get; set; }
        public bool Extension { get; set; }
        public virtual ICollection<TypeMetadata> GenericArguments { get; set; }
        public virtual ICollection<ParameterMetadata> Parameters { get; set; }
    }
}