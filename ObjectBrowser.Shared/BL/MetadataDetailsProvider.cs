using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectBrowser.Interfaces;
using ObjectBrowser.Models.Entities;

namespace ObjectBrowser.Shared.BL
{
    public class MetadataDetailsProvider : IMetadataDetailsProvider
    {
        public IEnumerable<(string key, string value)> GetDetails(TypeMetadata data)
        {
            yield return ("Access:",data.Modifiers.AccessLevel.ToString());
            yield return ("IsSealed:",data.Modifiers.IsSealed.ToString());
            yield return ("IsAbstract:",data.Modifiers.IsAbstract.ToString());
            yield return ("Namespace:",data.NamespaceName);
            yield return ("Attributes:",string.Join(",", data.Attributes.Select(attribute => attribute.TypeName)));
            yield return ("BaseType:",data.BaseType.TypeName);
            yield return ("Implements:",string.Join(",", data.ImplementedInterfaces.Select(attribute => attribute.TypeName)));
        }

        public IEnumerable<(string key, string value)> GetDetails(MethodMetadata data)
        {
            yield return ("Access:", data.Modifiers.AccessLevel.ToString());
            yield return ("IsVirtual:", data.Modifiers.IsVirtual.ToString());
            yield return ("IsAbstract:", data.Modifiers.IsAbstract.ToString());
            yield return ("IsStatic:", data.Modifiers.IsStatic.ToString());
            yield return ("IsExtension:", data.Extension.ToString());
            yield return ("Params:", string.Join(",", data.Parameters.Select(metadata => $"{metadata.TypeMetadata.TypeName} {metadata.Name}")));
        }
    }

 
}
