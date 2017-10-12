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
    internal class AssemblyMetadataExtractor : IAssemblyMetadataExtractor
    {
        private readonly INamespaceMetadataExtractor _namespaceMetadataExtractor;

        public AssemblyMetadataExtractor(INamespaceMetadataExtractor namespaceMetadataExtractor)
        {
            _namespaceMetadataExtractor = namespaceMetadataExtractor;
        }

        public AssemblyMetadata Extract(Assembly assembly, bool limitToRootNamepsace)
        {
            var data = new AssemblyMetadata
            {
                Name = assembly.ManifestModule.Name,
                RestrictToNamespace = limitToRootNamepsace ? GetRootNamespace(assembly.GetTypes().First().Namespace) : null
            };
            data.Namespaces = assembly.GetTypes()
                .Where(type => type.GetVisible())
                .GroupBy(type => type.GetNamespace())
                .OrderBy(group => group.Key)
                .Select(group => _namespaceMetadataExtractor.Extract(group.Key, group.Select(type => type), data))
                .ToList();

            return data;

        }

        private string GetRootNamespace(string source)
        {
            var pos = source.IndexOf('.');
            if (pos != -1)
                source = source.Substring(0, pos);
            return source;

        }

    }
}
