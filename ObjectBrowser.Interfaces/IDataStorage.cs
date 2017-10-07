using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectBrowser.Models.Entities;

namespace ObjectBrowser.Interfaces
{
    public interface IDataStorage
    {
        Task Save(AssemblyMetadata assembly);
        Task<AssemblyMetadata> Retrieve();
    }
}
