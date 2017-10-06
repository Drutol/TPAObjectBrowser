using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ObjectBrowser.Models.Entities;
using ObjectBrowser.Shared.Extensions;

namespace ObjectBrowser.Shared.BL
{
    public class AssemblyMetadataExtractor : IAssemblyMetadataExtractor
    {
        private readonly INamespaceMetadataExtractor _namespaceMetadataExtractor;

        public AssemblyMetadataExtractor(INamespaceMetadataExtractor namespaceMetadataExtractor)
        {
            _namespaceMetadataExtractor = namespaceMetadataExtractor;
        }

        public AssemblyMetadata Extract(Assembly assembly)
        {
            return new AssemblyMetadata
            {
                Name = assembly.ManifestModule.Name,
                Namespaces = assembly.GetTypes()
                    .Where(type => type.GetVisible())
                    .GroupBy(type => type.GetNamespace())
                    .OrderBy(group => group.Key)
                    .Select(group => _namespaceMetadataExtractor.Extract(group.Key, group.Select(type => type))).ToList(),
            };
        }

    }
}
