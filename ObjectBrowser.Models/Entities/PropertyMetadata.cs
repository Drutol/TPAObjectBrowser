﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ObjectBrowser.Models.Entities
{
    public class PropertyMetadata
    {
        public PropertyMetadata(string name, TypeMetadata typeMetadata)
        {
            Name = name;
            TypeMetadata = typeMetadata;
        }

        public AssemblyMetadata RootAssembly { get; set; }
        public string Name { get; set; }
        public TypeMetadata TypeMetadata { get; set; }
    }
}