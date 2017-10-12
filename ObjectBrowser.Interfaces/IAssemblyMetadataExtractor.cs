using System.Reflection;
using ObjectBrowser.Models.Entities;

namespace ObjectBrowser.Shared.BL
{
    public interface IAssemblyMetadataExtractor
    {
        AssemblyMetadata Extract(Assembly assembly, bool limitToRootNamepsace);
    }
}