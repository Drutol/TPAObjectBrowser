using System;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectBrowser.Models.Entities;
using ObjectBrowser.Models.Enums;
using ObjectBrowser.Shared.BL;
using ObjectBrowser.TestAssembly.BusinesLogic;

namespace ObjectBrowser.Test.SharedTest
{
    [TestClass]
    public class ExtractionTest
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
        public void TestExtraction()
        {
            Assert.AreEqual(_metadata.RegisteredTypes.Count(metadata => metadata.TypeKind == TypeKind.ClassType),10);
            Assert.AreEqual(_metadata.RegisteredTypes.Count(metadata => metadata.TypeKind == TypeKind.EnumType),1);
            Assert.AreEqual(_metadata.RegisteredTypes.Count(metadata => metadata.TypeKind == TypeKind.InterfaceType),1);
        }
    }
}
