using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectBrowser.Models.Entities;

namespace ObjectBrowser.Shared.BL
{
    public class NamespaceMetadataExtractor : INamespaceMetadataExtractor
    {
        private readonly ITypeMetadataExtractor _typeMetadataExtractor;

        public NamespaceMetadataExtractor(ITypeMetadataExtractor typeMetadataExtractor)
        {
            _typeMetadataExtractor = typeMetadataExtractor;
        }

        public NamespaceMetadata Extract(string name, IEnumerable<Type> types)
        {
            return new NamespaceMetadata(name, types.OrderBy(type => type.Name).Select(type => _typeMetadataExtractor.Extract(type)).ToList());
        }
    }
}
