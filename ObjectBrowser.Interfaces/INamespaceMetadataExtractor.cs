using System;
using System.Collections.Generic;
using ObjectBrowser.Models.Entities;

namespace ObjectBrowser.Shared.BL
{
    public interface INamespaceMetadataExtractor
    {
        NamespaceMetadata Extract(string name, IEnumerable<Type> types);
    }
}