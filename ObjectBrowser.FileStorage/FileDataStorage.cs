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
            using (MemoryStream memStm = new MemoryStream())
            {
                var serialzier = new DataContractSerializer(typeof(AssemblyMetadata));
                serialzier.WriteObject(memStm, assembly);


                memStm.Seek(0, SeekOrigin.Begin);

                using (var streamReader = new StreamReader(memStm))
                {
                    var writer = new StreamWriter(@"data.xml");
                    var str = await streamReader.ReadToEndAsync();
                    await writer.WriteAsync(str);
                    await writer.FlushAsync();
                    writer.Dispose();
                }
            }
            //XmlSerializer xs = new XmlSerializer(typeof(AssemblyMetadata));
            //TextWriter tw = 
            //xs.Serialize(tw,assembly);
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
