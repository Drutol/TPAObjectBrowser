using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectBrowser.Models.Entities;

namespace ObjectBrowser.Interfaces
{
    public interface IMetadataDetailsProvider
    {
        IEnumerable<(string key,string value)> GetDetails(TypeMetadata typeMetadata);
        IEnumerable<(string key,string value)> GetDetails(MethodMetadata typeMetadata);
    }
}
