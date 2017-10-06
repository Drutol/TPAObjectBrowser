using System;
using ObjectBrowser.Models.Entities;

namespace ObjectBrowser.Shared.BL
{
    public interface ITypeMetadataExtractor
    {
        TypeMetadata Extract(Type type);
    }
}