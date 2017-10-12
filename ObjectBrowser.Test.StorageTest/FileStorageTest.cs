using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectBrowser.DataAccess;
using ObjectBrowser.FileStorage;
using ObjectBrowser.Models.Entities;
using ObjectBrowser.Shared.BL;
using ObjectBrowser.TestAssembly.BusinesLogic;

namespace ObjectBrowser.Test.StorageTest
{
    [TestClass]
    public class FileStorageTest
    {
        private AssemblyMetadata _metadata;

        [TestInitialize]
        public void Init()
        {
            _metadata =
                new AssemblyMetadataExtractor(
                        new NamespaceMetadataExtractor(
                            new TypeMetadataExtractor(new MethodMetadataExtractor())))
                    .Extract(Assembly.GetAssembly(typeof(ServiceA)), true);
        }

        [TestMethod]
        public async Task TestFileStorage()
        {
            var storage = new FileDataStorage();
            await storage.Save(_metadata);
            var data = await storage.Retrieve();

            CollectionAssert.AreEqual(data.RegisteredTypes.Select(metadata => metadata.TypeName).ToList(),
                _metadata.RegisteredTypes.Select(metadata => metadata.TypeName).ToList());
        }
    }
}
