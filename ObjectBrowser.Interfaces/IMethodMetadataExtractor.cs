using System.Reflection;
using ObjectBrowser.Models.Entities;

namespace ObjectBrowser.Shared.BL
{
    public interface IMethodMetadataExtractor
    {
        MethodMetadata Extract(MethodBase method);
    }
}