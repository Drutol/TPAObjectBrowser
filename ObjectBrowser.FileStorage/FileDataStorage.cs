using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ObjectBrowser.Interfaces;
using ObjectBrowser.Models.Entities;

namespace ObjectBrowser.FileStorage
{
    [Export(typeof(IDataStorage))]
    public class FileDataStorage : IDataStorage
    {
        public async Task Save(AssemblyMetadata assembly)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                var serialzier = new DataContractSerializer(typeof(AssemblyMetadata));
                serialzier.WriteObject(memoryStream, assembly);


                memoryStream.Seek(0, SeekOrigin.Begin);

                using (var streamReader = new StreamReader(memoryStream))
                {
                    var writer = new StreamWriter(@"data.xml");
                    var str = await streamReader.ReadToEndAsync();
                    await writer.WriteAsync(str);
                    await writer.FlushAsync();
                    writer.Dispose();
                }
            }
        }

        public async Task<AssemblyMetadata> Retrieve()
        {
            using (var reader = File.OpenRead(@"data.xml"))
            {
                var serialzier = new DataContractSerializer(typeof(AssemblyMetadata));
                var obj = serialzier.ReadObject(reader);

                return obj as AssemblyMetadata;
            }
        }
    }
}
