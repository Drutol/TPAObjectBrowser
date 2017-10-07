using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectBrowser.Models.Entities
{
    public class FieldMetadata
    {
        public FieldMetadata(string name, TypeMetadata typeMetadata)
        {
            Name = name;
            TypeMetadata = typeMetadata;
        }

        public string Name { get; set; }
        public TypeMetadata TypeMetadata { get; set; }
    }
}
